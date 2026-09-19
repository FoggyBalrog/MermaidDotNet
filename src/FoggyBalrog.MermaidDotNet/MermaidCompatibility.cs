using FoggyBalrog.MermaidDotNet.Configuration.Model;

namespace FoggyBalrog.MermaidDotNet;

public static class MermaidCompatibility
{
    public static MermaidVersion MinimumSupportedVersion { get; } = MermaidVersion.V11_0;

    /// <summary>
    /// The latest Mermaid version tested by the parser integration suite, not a target-version ceiling.
    /// </summary>
    public static MermaidVersion LatestTestedVersion { get; } = MermaidVersion.V11_13;

    internal static void EnsureSupportedTarget(this MermaidDotNetOptions options)
    {
        if (options.CompatibilityMode is not MermaidCompatibilityMode.Strict and not MermaidCompatibilityMode.Unchecked)
        {
            throw MermaidException.InvalidConfiguration($"Unknown compatibility mode: {options.CompatibilityMode}.");
        }

        if (options.TargetMermaidVersion < MinimumSupportedVersion)
        {
            throw MermaidException.IncompatibleVersion("MermaidDotNet support", options.TargetMermaidVersion, MinimumSupportedVersion);
        }
    }

    internal static void EnsureCompatible(this MermaidDotNetOptions options, MermaidFeature feature)
    {
        if (options.CompatibilityMode == MermaidCompatibilityMode.Unchecked)
        {
            return;
        }

        MermaidFeatureInfo info = MermaidFeatureRegistry.All[feature];

        if (options.TargetMermaidVersion < info.MinimumVersion
            || (info.RemovedInVersion.HasValue && options.TargetMermaidVersion >= info.RemovedInVersion.Value))
        {
            throw MermaidException.IncompatibleVersion(info.Name, options.TargetMermaidVersion, info.MinimumVersion,
                removedInVersion: info.RemovedInVersion, migrationGuidance: info.MigrationGuidance);
        }
    }

    internal static void EnsureCompatible(this MermaidDotNetOptions options, MermaidFeature diagram, MermaidConfig? config)
    {
        options.EnsureCompatible(diagram);

        if (config is null || options.CompatibilityMode == MermaidCompatibilityMode.Unchecked)
        {
            return;
        }

        // Check presence, not truthiness: false and zero are serialized configuration too.
        // Inspect the current values on every Build, since callers may mutate the configuration.
        if (config.Elk?.CycleBreakingStrategy is not null)
        {
            options.EnsureCompatible(MermaidFeature.ElkCycleBreakingStrategy);
        }

        if (config.Kanban is not null)
        {
            options.EnsureCompatible(MermaidFeature.KanbanConfiguration);
        }

        if (config.Journey?.TitleColor is not null
            || config.Journey?.TitleFontFamily is not null
            || config.Journey?.TitleFontSize is not null)
        {
            options.EnsureCompatible(MermaidFeature.UserJourneyTitleStyling);
        }

        if (config.XYChart?.ShowDataLabel is not null)
        {
            options.EnsureCompatible(MermaidFeature.XYChartDataLabels);
        }

        if (config.Flowchart?.DefaultRenderer is not null)
        {
            options.EnsureCompatible(MermaidFeature.FlowchartDefaultRenderer);
        }

        if (config.Class?.DefaultRenderer is not null)
        {
            options.EnsureCompatible(MermaidFeature.ClassDefaultRenderer);
        }

        if (config.State?.DefaultRenderer is not null)
        {
            options.EnsureCompatible(MermaidFeature.StateDefaultRenderer);
        }
    }
}
