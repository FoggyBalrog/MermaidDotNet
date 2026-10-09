using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;
using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

// These tests validate emitted syntax with the pinned 11.13 parser, not historical runtimes.
// The 11.15 nested namespace boundary is covered by unit tests instead.
public class MermaidFeatureCompatibilityIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    public static IEnumerable<object[]> ExpandedShapes => Enum.GetValues<ExpandedNodeShape>().Select(shape => new object[] { shape });
    public static IEnumerable<object[]> Curves => Enum.GetValues<CurveStyle>().SelectMany(curve => new[] { new object[] { curve, false }, new object[] { curve, true } });
    public static IEnumerable<object[]> ParticipantShapes => Enum.GetValues<MemberType>()
        .Where(type => type is not MemberType.Actor and not MemberType.Participant)
        .SelectMany(type => new[] { new object[] { type, false, false }, new object[] { type, false, true }, new object[] { type, true, false } });

    [Theory]
    [MemberData(nameof(ExpandedShapes))]
    public async Task ExpandedShapes_AllValuesParseAtSupportedTarget(ExpandedNodeShape shape)
    {
        string diagram = Mermaid.Flowchart(options: new() { TargetMermaidVersion = MermaidVersion.V11_3 })
            .AddNodeWithExpandedShape("Expanded", out _, shape).Build();
        await AssertParses(diagram);
    }

    [Theory]
    [MemberData(nameof(Curves))]
    public async Task EdgeCurves_AllValuesParseForBothLinkApis(CurveStyle curve, bool chain)
    {
        var builder = Mermaid.Flowchart(options: new() { TargetMermaidVersion = MermaidVersion.V11_10 })
            .AddNode("From", out var from).AddNode("To", out var to);
        if (chain) builder.AddLinkChain([from], [to], out _, curveStyle: curve);
        else builder.AddLink(from, to, out _, curveStyle: curve);
        await AssertParses(builder.Build());
    }

    [Theory]
    [MemberData(nameof(ParticipantShapes))]
    public async Task ParticipantShapes_MetadataAndAliasesParseForEveryDeclarationPath(MemberType type, bool create, bool boxed)
    {
        var builder = Mermaid.SequenceDiagram(options: new() { TargetMermaidVersion = MermaidVersion.V11_13 })
            .AddMember("Sender", out var sender);
        Box? box = null;
        if (boxed) builder.AddBox("Group", out box);
        if (create) builder.SendCreateMessage(sender, "Display Name", out _, "Create", type);
        else builder.AddMember("Display Name", out _, type, box);
        string diagram = builder.Build();
        Assert.Contains($"m1@{{ \"type\" : \"{type.ToString().ToLowerInvariant()}\" }} as Display Name", diagram);
        await AssertParses(diagram);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GatedSyntax_ParsesWithStrictTargetOrUncheckedBypass(bool uncheckedMode)
    {
        var options = new MermaidDotNetOptions
        {
            TargetMermaidVersion = uncheckedMode ? MermaidVersion.V11_0 : MermaidVersion.V11_7,
            CompatibilityMode = uncheckedMode ? MermaidCompatibilityMode.Unchecked : MermaidCompatibilityMode.Strict
        };
        string gantt = Mermaid.GanttDiagram(options: options)
            .AddVerticalMarker("Marker", new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)).Build();
        string state = Mermaid.StateDiagram(options: options).AddState("State", out var item)
            .AddStateLink(item, "https://example.com", "Tooltip").Build();
        string packet = Mermaid.PacketDiagram(options: options with
            { TargetMermaidVersion = uncheckedMode ? MermaidVersion.V11_0 : MermaidVersion.V11_9 })
            .AddFieldWithBits(8, "Header").AddFieldWithBits(8, "Payload").Build();
        string classes = Mermaid.ClassDiagram(options: options with
            { TargetMermaidVersion = uncheckedMode ? MermaidVersion.V11_0 : MermaidVersion.V11_3 })
            .AddNamespace("Company.Product", ns => ns.AddClass("Item", out _)).Build();
        foreach (string diagram in new[] { gantt, state, packet, classes }) await AssertParses(diagram);
    }

    [Fact]
    public async Task IntroducedConfig_ParsesAtSupportedTarget()
    {
        var config = new MermaidConfig
        {
            Elk = new() { CycleBreakingStrategy = ElkCycleBreakingStrategy.DepthFirst },
            Kanban = new(),
            Journey = new() { TitleColor = "red", TitleFontFamily = "Arial", TitleFontSize = "24px" },
            XYChart = new() { ShowDataLabel = false }
        };
        string diagram = Mermaid.Flowchart(config: config, options: new() { TargetMermaidVersion = MermaidVersion.V11_7 })
            .AddNode("Node", out _).Build();
        await AssertParses(diagram);
    }

    private async Task AssertParses(string diagram)
    {
        var result = await toolingFixture.ValidateDiagramAsync(diagram);
        Assert.True(result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, result));
    }
}
