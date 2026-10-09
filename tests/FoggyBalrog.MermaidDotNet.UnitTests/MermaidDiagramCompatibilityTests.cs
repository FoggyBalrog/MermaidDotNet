using System.Reflection;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidDiagramCompatibilityTests
{
    private static readonly IReadOnlyDictionary<string, (string Header, int Minor, Func<MermaidDotNetOptions?, MermaidConfig?, Func<string>> Create)> Builders =
        new Dictionary<string, (string, int, Func<MermaidDotNetOptions?, MermaidConfig?, Func<string>>)>
        {
            [nameof(Mermaid.BlockDiagram)] = ("block", 10, (o, c) => Mermaid.BlockDiagram(options: o, config: c).Build),
            [nameof(Mermaid.ClassDiagram)] = ("classDiagram", 0, (o, c) => Mermaid.ClassDiagram(options: o, config: c).Build),
            [nameof(Mermaid.EntityRelationshipDiagram)] = ("erDiagram", 0, (o, c) => Mermaid.EntityRelationshipDiagram(options: o, config: c).Build),
            [nameof(Mermaid.Flowchart)] = ("flowchart TB", 0, (o, c) => Mermaid.Flowchart(options: o, config: c).Build),
            [nameof(Mermaid.GanttDiagram)] = ("gantt", 0, (o, c) => Mermaid.GanttDiagram(options: o, config: c).Build),
            [nameof(Mermaid.GitGraph)] = ("gitGraph", 0, (o, c) => Mermaid.GitGraph(options: o, config: c).Build),
            [nameof(Mermaid.KanbanDiagram)] = ("kanban", 4, (o, c) => Mermaid.KanbanDiagram(options: o, config: c).Build),
            [nameof(Mermaid.MindMap)] = ("mindmap", 0, (o, c) => Mermaid.MindMap("Root", options: o, config: c).Build),
            [nameof(Mermaid.PacketDiagram)] = ("packet", 9, (o, c) => Mermaid.PacketDiagram(options: o, config: c).Build),
            [nameof(Mermaid.PieChart)] = ("pie", 0, (o, c) => Mermaid.PieChart(options: o, config: c).Build),
            [nameof(Mermaid.QuadrantChart)] = ("quadrantChart", 0, (o, c) => Mermaid.QuadrantChart(options: o, config: c).Build),
            [nameof(Mermaid.RequirementDiagram)] = ("requirementDiagram", 0, (o, c) => Mermaid.RequirementDiagram(options: o, config: c).Build),
            [nameof(Mermaid.SankeyDiagram)] = ("sankey", 10, (o, c) => Mermaid.SankeyDiagram(options: o, config: c).Build),
            [nameof(Mermaid.SequenceDiagram)] = ("sequenceDiagram", 0, (o, c) => Mermaid.SequenceDiagram(options: o, config: c).Build),
            [nameof(Mermaid.StateDiagram)] = ("stateDiagram-v2", 0, (o, c) => Mermaid.StateDiagram(options: o, config: c).Build),
            [nameof(Mermaid.TimelineDiagram)] = ("timeline", 0, (o, c) => Mermaid.TimelineDiagram(options: o, config: c).Build),
            [nameof(Mermaid.UserJourneyDiagram)] = ("journey", 0, (o, c) => Mermaid.UserJourneyDiagram(options: o, config: c).Build),
            [nameof(Mermaid.XYChart)] = ("xychart", 10, (o, c) => Mermaid.XYChart(options: o, config: c).Build)
        };

    public static IEnumerable<object[]> BuilderNames => Builders.Keys.Select(name => new object[] { name });

    [Fact]
    public void BuildCoverage_IncludesAllEighteenFactories()
    {
        Assert.Equal(18, Builders.Count);
        Assert.Equal(typeof(Mermaid).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(method => method.Name).Order(), Builders.Keys.Order());
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Build_EnforcesDiagramBoundaryAndUsesStableHeader(string name)
    {
        var (header, minor, create) = Builders[name];
        foreach (bool validateInputs in new[] { false, true })
        {
            var options = new MermaidDotNetOptions { TargetMermaidVersion = new(11, minor), ValidateInputs = validateInputs };
            Assert.Equal(header, create(options, null)().Split('\n')[0].TrimEnd('\r'));
            Assert.Equal(header, create(options with { TargetMermaidVersion = new(99, 0) }, null)().Split('\n')[0].TrimEnd('\r'));
            if (minor == 0) continue;

            var below = options with { TargetMermaidVersion = new(11, minor - 1, 99) };
            Func<string> build = create(below, null);
            AssertIncompatible(build);
            AssertIncompatible(build);
            Assert.StartsWith(header, create(below with { CompatibilityMode = MermaidCompatibilityMode.Unchecked }, null)());
        }
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Build_DefaultStrictTargetRejectsOnlyNewerDiagramSyntax(string name)
    {
        var (header, minor, create) = Builders[name];
        var build = create(null, null);
        if (minor > 4)
            AssertIncompatible(build);
        else
            Assert.StartsWith(header, build());
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Build_UsesSnapshottedTargetAndCompatibilityMode(string name)
    {
        var options = new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_13 };
        var build = Builders[name].Create(options, null);
        options.TargetMermaidVersion = MermaidVersion.V11_0;
        options.CompatibilityMode = MermaidCompatibilityMode.Unchecked;
        Assert.StartsWith(Builders[name].Header, build());
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void EveryBuild_RechecksMutableConfigurationIncludingUnrelatedSections(string name)
    {
        foreach (string section in new[] { "Flowchart", "Class", "State" })
        {
            foreach (bool validateInputs in new[] { false, true })
            {
                var config = new MermaidConfig
                {
                    Flowchart = new(), Class = new(), State = new()
                };
                var options = new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V12_0, ValidateInputs = validateInputs };
                var build = Builders[name].Create(options, config);
                string before = build();

                SetRenderer(config, section, Rendered.Elk);
                var exception = AssertIncompatible(build);
                Assert.Contains("< 12.0", exception.Message);
                Assert.Contains("layout", exception.Message, StringComparison.OrdinalIgnoreCase);
                AssertIncompatible(build);

                SetRenderer(config, section, null);
                Assert.Equal(before, build());

                SetRenderer(config, section, Rendered.Elk);
                var uncheckedBuild = Builders[name].Create(options with { CompatibilityMode = MermaidCompatibilityMode.Unchecked }, config);
                Assert.Contains("defaultRenderer: elk", uncheckedBuild());
                var beforeRemoval = Builders[name].Create(options with { TargetMermaidVersion = new(11, 99, 99) }, config);
                Assert.Contains("defaultRenderer: elk", beforeRemoval());
            }
        }
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void EveryBuild_RejectsConfigurationMutatedAfterConstruction(string name)
    {
        var config = new MermaidConfig();
        var build = Builders[name].Create(new() { TargetMermaidVersion = MermaidVersion.V12_0 }, config);
        config.Flowchart = new() { DefaultRenderer = Rendered.Elk };
        AssertIncompatible(build);
        config.Flowchart = null;
        Assert.StartsWith(Builders[name].Header, build());
    }

    private static void SetRenderer(MermaidConfig config, string section, Rendered? value)
    {
        switch (section)
        {
            case "Flowchart": config.Flowchart!.DefaultRenderer = value; break;
            case "Class": config.Class!.DefaultRenderer = value; break;
            case "State": config.State!.DefaultRenderer = value; break;
        }
    }

    private static MermaidException AssertIncompatible(Func<string> build)
    {
        var exception = Assert.Throws<MermaidException>(() => build());
        Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
        return exception;
    }
}
