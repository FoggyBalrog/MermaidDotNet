using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

internal sealed class WorkerSession : IAsyncDisposable
{
    private readonly Process _process;
    private readonly StreamWriter _standardInput;
    private readonly StreamReader _standardOutput;
    private readonly SemaphoreSlim _requestLock = new(1, 1);
    private readonly StringBuilder _standardError = new();
    private readonly Task _standardErrorPump;
    private int _requestId;

    private WorkerSession(Process process)
    {
        _process = process;
        _standardInput = process.StandardInput;
        _standardOutput = process.StandardOutput;
        _standardErrorPump = PumpStandardErrorAsync();
    }

    public bool HasExited => _process.HasExited;

    public static async Task<WorkerSession> StartAsync(string nodeToolingDirectory)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "node",
            WorkingDirectory = nodeToolingDirectory,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        startInfo.ArgumentList.Add(Path.Combine(nodeToolingDirectory, "validate-mermaid.mjs"));

        var process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Failed to start Mermaid worker process.");

        var session = new WorkerSession(process);
        var readyLine = await session._standardOutput.ReadLineAsync();

        if (readyLine is null)
        {
            await session.DisposeAsync();
            throw new InvalidOperationException($"Mermaid worker exited before signalling readiness.{Environment.NewLine}{session.GetWorkerErrorOutput()}");
        }

        using var readyDocument = JsonDocument.Parse(readyLine);
        if (!readyDocument.RootElement.TryGetProperty("ready", out var readyProperty) || !readyProperty.GetBoolean())
        {
            await session.DisposeAsync();
            throw new InvalidOperationException($"Mermaid worker returned an invalid readiness payload: {readyLine}");
        }

        return session;
    }

    public async Task<ProcessResult> ValidateDiagramAsync(string diagram)
    {
        await _requestLock.WaitAsync();

        try
        {
            ThrowIfExited();

            var requestId = Interlocked.Increment(ref _requestId);
            var payload = JsonSerializer.Serialize(new WorkerRequest(requestId, diagram), MermaidToolingFixture.WorkerJsonOptions);

            await _standardInput.WriteLineAsync(payload);
            await _standardInput.FlushAsync();

            var responseLine = await _standardOutput.ReadLineAsync();
            if (responseLine is null)
            {
                ThrowIfExited();
                throw new InvalidOperationException("Mermaid worker closed stdout unexpectedly.");
            }

            var response = JsonSerializer.Deserialize<WorkerResponse>(responseLine, MermaidToolingFixture.WorkerJsonOptions)
                ?? throw new InvalidOperationException($"Mermaid worker returned an invalid response: {responseLine}");

            if (response.Id is not null && response.Id != requestId)
            {
                throw new InvalidOperationException($"Mermaid worker response id mismatch. Expected {requestId}, received {response.Id}.");
            }

            if (response.Ok)
            {
                return new ProcessResult(0, responseLine, string.Empty);
            }

            return new ProcessResult(1, responseLine, BuildErrorMessage(response));
        }
        finally
        {
            _requestLock.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (!_process.HasExited)
            {
                _standardInput.Close();
                await _process.WaitForExitAsync();
            }
        }
        finally
        {
            _process.Dispose();
            _requestLock.Dispose();
            await _standardErrorPump;
        }
    }

    private async Task PumpStandardErrorAsync()
    {
        while (await _process.StandardError.ReadLineAsync() is { } line)
        {
            lock (_standardError)
            {
                _standardError.AppendLine(line);
            }
        }
    }

    private void ThrowIfExited()
    {
        if (_process.HasExited)
        {
            throw new InvalidOperationException(
                $"""
                 Mermaid worker process exited unexpectedly with code {_process.ExitCode}.
                 Worker standard error:
                 {GetWorkerErrorOutput()}
                 """);
        }
    }

    private string BuildErrorMessage(WorkerResponse response)
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(response.LocationText))
        {
            builder.AppendLine($"Location: {response.LocationText}");
        }

        if (!string.IsNullOrWhiteSpace(response.Message))
        {
            builder.Append(response.Message);
        }

        return builder.ToString();
    }

    private string GetWorkerErrorOutput()
    {
        lock (_standardError)
        {
            return _standardError.Length == 0
                ? "<empty>"
                : _standardError.ToString().TrimEnd();
        }
    }

    private sealed record WorkerRequest(int Id, string Diagram);

    private sealed class WorkerResponse
    {
        public int? Id { get; init; }

        public bool Ok { get; init; }

        public string? DiagramType { get; init; }

        public string? Message { get; init; }

        [JsonPropertyName("location")]
        public WorkerLocation? LocationData { get; init; }

        public string LocationText => LocationData is null
            ? string.Empty
            : $"{LocationData.FirstLine}:{LocationData.FirstColumn} - {LocationData.LastLine}:{LocationData.LastColumn}";
    }

    private sealed class WorkerLocation
    {
        public int FirstLine { get; init; }

        public int FirstColumn { get; init; }

        public int LastLine { get; init; }

        public int LastColumn { get; init; }
    }
}
