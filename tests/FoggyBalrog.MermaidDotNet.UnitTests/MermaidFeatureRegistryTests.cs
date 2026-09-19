namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidFeatureRegistryTests
{
    public static TheoryData<string, string, string?> Features => new()
    {
        { "BlockDiagram", "11.10", null },
        { "ClassDiagram", "11.0", null },
        { "EntityRelationshipDiagram", "11.0", null },
        { "Flowchart", "11.0", null },
        { "GanttDiagram", "11.0", null },
        { "GitGraph", "11.0", null },
        { "KanbanDiagram", "11.4", null },
        { "MindMap", "11.0", null },
        { "PacketDiagram", "11.9", null },
        { "PieChart", "11.0", null },
        { "QuadrantChart", "11.0", null },
        { "RequirementDiagram", "11.0", null },
        { "SankeyDiagram", "11.10", null },
        { "SequenceDiagram", "11.0", null },
        { "StateDiagram", "11.0", null },
        { "TimelineDiagram", "11.0", null },
        { "UserJourneyDiagram", "11.0", null },
        { "XYChart", "11.10", null },
        { "FlowchartExpandedNodeShapes", "11.3", null },
        { "FlowchartEdgeCurves", "11.10", null },
        { "GanttVerticalMarkers", "11.7", null },
        { "PacketBitsSyntax", "11.7", null },
        { "StateHyperlinks", "11.7", null },
        { "SequenceParticipantShapesWithAliases", "11.13", null },
        { "ClassDottedNamespaces", "11.3", null },
        { "ClassNestedNamespaces", "11.15", null },
        { "ERMultilineRelationshipLabels", "11.1", null },
        { "ElkCycleBreakingStrategy", "11.1", null },
        { "KanbanConfiguration", "11.4", null },
        { "UserJourneyTitleStyling", "11.7", null },
        { "XYChartDataLabels", "11.7", null },
        { "FlowchartDefaultRenderer", "11.0", "12.0" },
        { "ClassDefaultRenderer", "11.0", "12.0" },
        { "StateDefaultRenderer", "11.0", "12.0" }
    };

    [Fact]
    public void Registry_AndBoundaryCases_CoverEveryFeatureExactlyOnce()
    {
        var features = Enum.GetValues<MermaidFeature>().OrderBy(feature => feature).ToArray();
        Assert.Equal(features, MermaidFeatureRegistry.All.Keys.OrderBy(feature => feature));
        Assert.Equal(features, Features.Select(row => Enum.Parse<MermaidFeature>((string)row[0])).OrderBy(feature => feature));
        Assert.Equal(MermaidFeatureRegistry.All.Count,
            MermaidFeatureRegistry.All.Values.Select(info => info.Name).Distinct().Count());
    }

    [Theory]
    [MemberData(nameof(Features))]
    public void Registry_HasExpectedMetadataAndInclusiveIntroductionBoundary(string name, string minimum, string? removed)
    {
        var feature = Enum.Parse<MermaidFeature>(name);
        var info = MermaidFeatureRegistry.All[feature];
        var version = MermaidVersion.Parse(minimum);
        Assert.False(string.IsNullOrWhiteSpace(info.Name));
        Assert.Equal(version, info.MinimumVersion);
        Assert.Equal(removed is null ? (MermaidVersion?)null : MermaidVersion.Parse(removed), info.RemovedInVersion);

        foreach (bool validateInputs in new[] { false, true })
        {
            var options = new MermaidDotNetOptions { ValidateInputs = validateInputs, TargetMermaidVersion = version };
            options.EnsureCompatible(feature);
            (options with { TargetMermaidVersion = new(version.Major, version.Minor, 1) }).EnsureCompatible(feature);

            var below = version.Minor == 0
                ? new MermaidVersion(version.Major - 1, 99, 99)
                : new MermaidVersion(version.Major, version.Minor - 1, 99);
            var exception = Assert.Throws<MermaidException>(() =>
                (options with { TargetMermaidVersion = below }).EnsureCompatible(feature));
            Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
            Assert.Contains($"Feature '{info.Name}' is not compatible with target Mermaid {below}.", exception.Message);
            Assert.Contains($"Required version range: >= {minimum}", exception.Message);
            Assert.Contains("TargetMermaidVersion", exception.Message);
        }
    }

    [Theory]
    [MemberData(nameof(Features))]
    public void EnsureCompatible_EnforcesRemovalButDoesNotImposeLatestTestedCeiling(string name, string minimum, string? removed)
    {
        var feature = Enum.Parse<MermaidFeature>(name);
        var info = MermaidFeatureRegistry.All[feature];
        if (removed is null)
        {
            new MermaidDotNetOptions { TargetMermaidVersion = new(99, 0) }.EnsureCompatible(feature);
            return;
        }

        Assert.False(string.IsNullOrWhiteSpace(info.MigrationGuidance));
        new MermaidDotNetOptions { TargetMermaidVersion = new(11, 99, 99) }.EnsureCompatible(feature);
        foreach (var target in new[] { MermaidVersion.Parse(removed), new MermaidVersion(12, 0, 1), new MermaidVersion(99, 0) })
        {
            foreach (bool validateInputs in new[] { false, true })
            {
                var exception = Assert.Throws<MermaidException>(() =>
                    new MermaidDotNetOptions { TargetMermaidVersion = target, ValidateInputs = validateInputs }.EnsureCompatible(feature));
                Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
                Assert.Contains($"Required version range: >= {minimum} and < {removed}.", exception.Message);
                Assert.Contains(info.MigrationGuidance!, exception.Message);
            }
        }
    }

    [Theory]
    [MemberData(nameof(Features))]
    public void Unchecked_BypassesEveryIntroductionAndRemoval(string name, string minimum, string? removed)
    {
        var feature = Enum.Parse<MermaidFeature>(name);
        foreach (var target in new[] { MermaidVersion.V11_0, MermaidVersion.Parse(minimum), MermaidVersion.Parse(removed ?? "12.0"), new MermaidVersion(99, 0) })
        {
            new MermaidDotNetOptions
            {
                TargetMermaidVersion = target,
                CompatibilityMode = MermaidCompatibilityMode.Unchecked
            }.EnsureCompatible(feature);
        }
    }
}
