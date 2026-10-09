namespace FoggyBalrog.MermaidDotNet;

public record MermaidDotNetOptions
{
    public bool ValidateInputs { get; set; } = true;

    public bool SanitizeInputs { get; set; } = false;

    public MermaidVersion TargetMermaidVersion { get; set; } = MermaidVersion.V11_4;

    public MermaidCompatibilityMode CompatibilityMode { get; set; } = MermaidCompatibilityMode.Strict;

    internal static MermaidDotNetOptions Snapshot(MermaidDotNetOptions? options)
    {
        var snapshot = options is null
            ? new MermaidDotNetOptions()
            : options with { }; // This is a shallow copy.

        snapshot.EnsureSupportedTarget();
        return snapshot;
    }
}
