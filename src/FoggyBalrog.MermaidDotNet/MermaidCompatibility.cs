namespace FoggyBalrog.MermaidDotNet;

public static class MermaidCompatibility
{
    public static MermaidVersion MinimumSupportedVersion { get; } = MermaidVersion.V11_0;

    public static MermaidVersion LatestTestedVersion { get; } = MermaidVersion.V11_13;

    internal static void ValidateTarget(MermaidDotNetOptions options)
    {
        if (options.CompatibilityMode is not MermaidCompatibilityMode.Strict and not MermaidCompatibilityMode.Unchecked)
        {
            throw MermaidException.InvalidConfiguration($"Unknown compatibility mode: {options.CompatibilityMode}.");
        }

        MermaidVersion? maximumVersion = options.CompatibilityMode == MermaidCompatibilityMode.Strict
            ? LatestTestedVersion
            : null;

        if (options.TargetMermaidVersion < MinimumSupportedVersion ||
            (maximumVersion.HasValue && options.TargetMermaidVersion > maximumVersion.Value))
        {
            throw MermaidException.IncompatibleVersion("MermaidDotNet support", options.TargetMermaidVersion, MinimumSupportedVersion, maximumVersion);
        }
    }

    internal static void RequireVersion(
        MermaidDotNetOptions options,
        string feature,
        MermaidVersion minimumVersion,
        MermaidVersion? removedInVersion = null)
    {
        if (options.CompatibilityMode == MermaidCompatibilityMode.Unchecked)
        {
            return;
        }

        if (options.TargetMermaidVersion < minimumVersion
            || (removedInVersion.HasValue && options.TargetMermaidVersion >= removedInVersion.Value))
        {
            throw MermaidException.IncompatibleVersion(feature, options.TargetMermaidVersion, minimumVersion, removedInVersion: removedInVersion);
        }
    }
}
