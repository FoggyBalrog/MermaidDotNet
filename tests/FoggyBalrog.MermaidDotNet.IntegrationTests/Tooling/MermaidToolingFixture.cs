using System.Diagnostics;
using System.Text.Json;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public sealed class MermaidToolingFixture : IAsyncLifetime
{
    private static readonly SemaphoreSlim ToolingInitializationLock = new(1, 1);
    internal static readonly JsonSerializerOptions WorkerJsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
    };
    private static WorkerSession? _sharedSession;

    public string ProjectDirectory { get; } = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    private string NodeToolingDirectory => Path.Combine(ProjectDirectory, "Tooling", "Node");

    public async Task InitializeAsync()
    {
        await EnsureToolingAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task<ProcessResult> ValidateDiagramAsync(string diagram)
    {
        var session = await EnsureToolingAsync();
        return await session.ValidateDiagramAsync(diagram);
    }

    public string FormatParsingErrorMessage(string diagram, ProcessResult result)
    {
        return $"""
             Mermaid parser validation failed.
             Diagram:
             {diagram}

             Standard output:
             {FormatOutput(result.StandardOutput)}

             Standard error:
             {FormatOutput(result.StandardError)}
             """;
    }

    public string GetDiagramType(ProcessResult result)
    {
        using var document = JsonDocument.Parse(result.StandardOutput);
        return document.RootElement.GetProperty("diagramType").GetString()
            ?? throw new InvalidOperationException("Mermaid parser output did not contain a diagram type.");
    }

    public static string FormatOutput(string text)
    {
        return string.IsNullOrWhiteSpace(text) ? "<empty>" : text.TrimEnd();
    }

    private async Task<ProcessResult> RunProcessAsync(string fileName, IReadOnlyList<string> arguments)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            WorkingDirectory = NodeToolingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        foreach (var argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        using var process = Process.Start(startInfo)
            ?? throw new InvalidOperationException($"Failed to start process '{fileName}'.");

        var standardOutputTask = process.StandardOutput.ReadToEndAsync();
        var standardErrorTask = process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        return new ProcessResult(process.ExitCode, await standardOutputTask, await standardErrorTask);
    }

    private async Task<WorkerSession> EnsureToolingAsync()
    {
        await ToolingInitializationLock.WaitAsync();

        try
        {
            if (!File.Exists(Path.Combine(NodeToolingDirectory, "node_modules", "mermaid", "package.json")))
            {
                var result = await RunProcessAsync("npm", ["ci", "--no-fund", "--no-audit"]);

                if (result.ExitCode != 0)
                {
                    throw new InvalidOperationException(
                        $"""
                         npm ci failed for the Mermaid integration test project.
                         Standard output:
                         {FormatOutput(result.StandardOutput)}

                         Standard error:
                         {FormatOutput(result.StandardError)}
                         """);
                }
            }

            var session = _sharedSession;
            if (session is not null && !session.HasExited)
            {
                return session;
            }

            if (session is not null)
            {
                await session.DisposeAsync();
            }

            _sharedSession = await WorkerSession.StartAsync(NodeToolingDirectory);
            return _sharedSession;
        }
        finally
        {
            ToolingInitializationLock.Release();
        }
    }
}
