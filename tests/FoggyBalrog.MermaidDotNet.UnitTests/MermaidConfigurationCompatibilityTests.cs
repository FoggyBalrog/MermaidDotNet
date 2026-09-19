using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidConfigurationCompatibilityTests
{
    public static TheoryData<string, int, string> IntroducedProperties => new()
    {
        { "Elk.CycleBreakingStrategy", 1, "cycleBreakingStrategy:" },
        { "Kanban", 4, "kanban:" },
        { "Journey.TitleColor", 7, "titleColor:" },
        { "Journey.TitleFontFamily", 7, "titleFontFamily:" },
        { "Journey.TitleFontSize", 7, "titleFontSize:" },
        { "XYChart.ShowDataLabel.True", 7, "showDataLabel: true" },
        { "XYChart.ShowDataLabel.False", 7, "showDataLabel: false" }
    };

    [Theory]
    [MemberData(nameof(IntroducedProperties))]
    public void Build_EnforcesEachConfigIntroductionAtItsBoundary(string property, int minor, string yaml)
    {
        foreach (bool validateInputs in new[] { false, true })
        {
            var config = new MermaidConfig();
            SetProperty(config, property);
            var options = new MermaidDotNetOptions { TargetMermaidVersion = new(11, minor - 1, 99), ValidateInputs = validateInputs };
            var builder = Mermaid.Flowchart(config: config, options: options);
            var exception = Assert.Throws<MermaidException>(() => builder.Build());
            Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
            Assert.Contains($">= 11.{minor}", exception.Message);
            Assert.Contains(yaml, Mermaid.Flowchart(config: config, options: options with { TargetMermaidVersion = new(11, minor) }).Build());
            Assert.Contains(yaml, Mermaid.Flowchart(config: config, options: options with { TargetMermaidVersion = new(11, minor, 1) }).Build());
            Assert.Contains(yaml, Mermaid.Flowchart(config: config, options: options with { CompatibilityMode = MermaidCompatibilityMode.Unchecked }).Build());
        }
    }

    [Theory]
    [MemberData(nameof(IntroducedProperties))]
    public void Build_RechecksIntroducedPropertiesAfterPreviouslySuccessfulBuild(string property, int minor, string yaml)
    {
        var config = new MermaidConfig();
        var builder = Mermaid.Flowchart(config: config, options: new() { TargetMermaidVersion = new(11, minor - 1, 99) });
        string before = builder.Build();
        Assert.DoesNotContain(yaml, before);
        SetProperty(config, property);
        var exception = Assert.Throws<MermaidException>(() => builder.Build());
        Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
        config.Elk = null;
        config.Kanban = null;
        config.Journey = null;
        config.XYChart = null;
        Assert.Equal(before, builder.Build());
    }

    [Fact]
    public void Build_AllowsUnsetGatedPropertiesInExistingSectionsAtBaseline()
    {
        var config = new MermaidConfig
        {
            Elk = new(), Journey = new(), XYChart = new(), Flowchart = new(), Class = new(), State = new()
        };
        Mermaid.Flowchart(config: config, options: new() { TargetMermaidVersion = MermaidVersion.V11_0 }).Build();
        Mermaid.Flowchart(config: config, options: new() { TargetMermaidVersion = MermaidVersion.V12_0 }).Build();
    }

    [Theory]
    [InlineData("Journey.TitleColor")]
    [InlineData("Journey.TitleFontFamily")]
    [InlineData("Journey.TitleFontSize")]
    [InlineData("XYChart.ShowDataLabel.True")]
    [InlineData("XYChart.ShowDataLabel.False")]
    public void Build_DefaultStrictOptionsRejectNewConfiguration(string property)
    {
        var config = new MermaidConfig();
        SetProperty(config, property);
        var exception = Assert.Throws<MermaidException>(() => Mermaid.Flowchart(config: config).Build());
        Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
    }

    [Fact]
    public void ElkCycleBreakingStrategy_AllValuesAreGatedIncludingDefaultEnumValue()
    {
        foreach (var strategy in Enum.GetValues<ElkCycleBreakingStrategy>())
        {
            var config = new MermaidConfig { Elk = new() { CycleBreakingStrategy = strategy } };
            var exception = Assert.Throws<MermaidException>(() => Mermaid.Flowchart(config: config,
                options: new() { TargetMermaidVersion = MermaidVersion.V11_0 }).Build());
            Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
            Assert.Contains("cycleBreakingStrategy:", Mermaid.Flowchart(config: config,
                options: new() { TargetMermaidVersion = new(11, 1) }).Build());
        }
    }

    [Fact]
    public void DefaultRenderer_AllValuesAreRemovedInAllThreeSections()
    {
        foreach (var renderer in Enum.GetValues<Rendered>())
        {
            MermaidConfig[] configs =
            [
                new() { Flowchart = new() { DefaultRenderer = renderer } },
                new() { Class = new() { DefaultRenderer = renderer } },
                new() { State = new() { DefaultRenderer = renderer } }
            ];
            foreach (var config in configs)
            {
                var exception = Assert.Throws<MermaidException>(() => Mermaid.Flowchart(config: config,
                    options: new() { TargetMermaidVersion = MermaidVersion.V12_0 }).Build());
                Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
                Assert.Contains("defaultRenderer:", Mermaid.Flowchart(config: config,
                    options: new() { TargetMermaidVersion = new(11, 99, 99) }).Build());
            }
        }
    }

    private static void SetProperty(MermaidConfig config, string property)
    {
        switch (property)
        {
            case "Elk.CycleBreakingStrategy": config.Elk = new() { CycleBreakingStrategy = ElkCycleBreakingStrategy.DepthFirst }; break;
            case "Kanban": config.Kanban = new(); break;
            case "Journey.TitleColor": config.Journey = new() { TitleColor = "red" }; break;
            case "Journey.TitleFontFamily": config.Journey = new() { TitleFontFamily = "Arial" }; break;
            case "Journey.TitleFontSize": config.Journey = new() { TitleFontSize = "24px" }; break;
            case "XYChart.ShowDataLabel.True": config.XYChart = new() { ShowDataLabel = true }; break;
            case "XYChart.ShowDataLabel.False": config.XYChart = new() { ShowDataLabel = false }; break;
            default: throw new ArgumentOutOfRangeException(nameof(property));
        }
    }
}
