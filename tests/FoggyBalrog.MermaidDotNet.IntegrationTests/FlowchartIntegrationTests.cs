using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class FlowchartDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .Flowchart()
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildNodesWithDifferentShapes()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out _, NodeShape.Rectangle)
            .AddNode("N2", out _, NodeShape.RoundEdges)
            .AddNode("N3", out _, NodeShape.Stadium)
            .AddNode("N4", out _, NodeShape.Subroutine)
            .AddNode("N5", out _, NodeShape.Cylindrical)
            .AddNode("N6", out _, NodeShape.Circle)
            .AddNode("N7", out _, NodeShape.DoubleCircle)
            .AddNode("N8", out _, NodeShape.Asymmetric)
            .AddNode("N9", out _, NodeShape.Rhombus)
            .AddNode("N10", out _, NodeShape.Hexagon)
            .AddNode("N11", out _, NodeShape.Parallelogram)
            .AddNode("N12", out _, NodeShape.ParallelogramAlt)
            .AddNode("N13", out _, NodeShape.Trapezoid)
            .AddNode("N14", out _, NodeShape.TrapezoidAlt)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildNodesWithDifferentExpandedShapes()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNodeWithExpandedShape("N1", out _, ExpandedNodeShape.NotchRect)
            .AddNodeWithExpandedShape("N2", out _, ExpandedNodeShape.Hourglass)
            .AddNodeWithExpandedShape("N3", out _, ExpandedNodeShape.Bolt)
            .AddNodeWithExpandedShape("N4", out _, ExpandedNodeShape.Brace)
            .AddNodeWithExpandedShape("N5", out _, ExpandedNodeShape.BraceR)
            .AddNodeWithExpandedShape("N6", out _, ExpandedNodeShape.Braces)
            .AddNodeWithExpandedShape("N7", out _, ExpandedNodeShape.LeanR)
            .AddNodeWithExpandedShape("N8", out _, ExpandedNodeShape.LeanL)
            .AddNodeWithExpandedShape("N9", out _, ExpandedNodeShape.Cyl)
            .AddNodeWithExpandedShape("N10", out _, ExpandedNodeShape.Diam)
            .AddNodeWithExpandedShape("N11", out _, ExpandedNodeShape.Delay)
            .AddNodeWithExpandedShape("N12", out _, ExpandedNodeShape.HCyl)
            .AddNodeWithExpandedShape("N13", out _, ExpandedNodeShape.LinCyl)
            .AddNodeWithExpandedShape("N14", out _, ExpandedNodeShape.CurvTrap)
            .AddNodeWithExpandedShape("N15", out _, ExpandedNodeShape.DivRect)
            .AddNodeWithExpandedShape("N16", out _, ExpandedNodeShape.Doc)
            .AddNodeWithExpandedShape("N17", out _, ExpandedNodeShape.Rounded)
            .AddNodeWithExpandedShape("N18", out _, ExpandedNodeShape.Tri)
            .AddNodeWithExpandedShape("N19", out _, ExpandedNodeShape.Fork)
            .AddNodeWithExpandedShape("N20", out _, ExpandedNodeShape.WinPane)
            .AddNodeWithExpandedShape("N21", out _, ExpandedNodeShape.FCirc)
            .AddNodeWithExpandedShape("N22", out _, ExpandedNodeShape.LinDoc)
            .AddNodeWithExpandedShape("N23", out _, ExpandedNodeShape.LinRect)
            .AddNodeWithExpandedShape("N24", out _, ExpandedNodeShape.NotchPent)
            .AddNodeWithExpandedShape("N25", out _, ExpandedNodeShape.FlipTri)
            .AddNodeWithExpandedShape("N26", out _, ExpandedNodeShape.SlRect)
            .AddNodeWithExpandedShape("N27", out _, ExpandedNodeShape.TrapT)
            .AddNodeWithExpandedShape("N28", out _, ExpandedNodeShape.Docs)
            .AddNodeWithExpandedShape("N29", out _, ExpandedNodeShape.StRect)
            .AddNodeWithExpandedShape("N30", out _, ExpandedNodeShape.Odd)
            .AddNodeWithExpandedShape("N31", out _, ExpandedNodeShape.Flag)
            .AddNodeWithExpandedShape("N32", out _, ExpandedNodeShape.Hex)
            .AddNodeWithExpandedShape("N33", out _, ExpandedNodeShape.TrapB)
            .AddNodeWithExpandedShape("N34", out _, ExpandedNodeShape.Rect)
            .AddNodeWithExpandedShape("N35", out _, ExpandedNodeShape.Circle)
            .AddNodeWithExpandedShape("N36", out _, ExpandedNodeShape.SmCirc)
            .AddNodeWithExpandedShape("N37", out _, ExpandedNodeShape.DblCirc)
            .AddNodeWithExpandedShape("N38", out _, ExpandedNodeShape.FrCirc)
            .AddNodeWithExpandedShape("N39", out _, ExpandedNodeShape.BowRect)
            .AddNodeWithExpandedShape("N40", out _, ExpandedNodeShape.FrRect)
            .AddNodeWithExpandedShape("N41", out _, ExpandedNodeShape.CrossCirc)
            .AddNodeWithExpandedShape("N42", out _, ExpandedNodeShape.TagDoc)
            .AddNodeWithExpandedShape("N43", out _, ExpandedNodeShape.TagRect)
            .AddNodeWithExpandedShape("N44", out _, ExpandedNodeShape.Stadium)
            .AddNodeWithExpandedShape("N45", out _, ExpandedNodeShape.Text)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildMarkdownNode()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddMarkdownNode("N1", out _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithDifferentOrientations()
    {
        string diagram1 = Mermaid
            .Flowchart(orientation: FlowchartOrientation.RightToLeft)
            .Build();

        string diagram2 = Mermaid
            .Flowchart(orientation: FlowchartOrientation.LeftToRight)
            .Build();

        string diagram3 = Mermaid
            .Flowchart(orientation: FlowchartOrientation.BottomToTop)
            .Build();

        string diagram4 = Mermaid
            .Flowchart(orientation: FlowchartOrientation.TopToBottom)
            .Build();

        var diagram1Result = await toolingFixture.ValidateDiagramAsync(diagram1);

        Assert.True(diagram1Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram1, diagram1Result));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagram1Result));

        var diagram2Result = await toolingFixture.ValidateDiagramAsync(diagram2);

        Assert.True(diagram2Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram2, diagram2Result));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagram2Result));

        var diagram3Result = await toolingFixture.ValidateDiagramAsync(diagram3);

        Assert.True(diagram3Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram3, diagram3Result));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagram3Result));

        var diagram4Result = await toolingFixture.ValidateDiagramAsync(diagram4);

        Assert.True(diagram4Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram4, diagram4Result));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagram4Result));
    }

    [Fact]
    public async Task CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddLink(n1, n2, out _, "some text")
            .AddLink(n2, n3, out _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAllLinkStyles()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node n6)
            .AddNode("N7", out Node n7)
            .AddNode("N8", out Node n8)
            .AddNode("N9", out Node n9)
            .AddNode("N10", out Node n10)
            .AddNode("N11", out Node n11)
            .AddNode("N12", out Node n12)
            .AddLink(n1, n2, out _, "l1", LinkLineStyle.Solid, LinkEnding.Arrow)
            .AddLink(n2, n3, out _, "l2", LinkLineStyle.Dotted, LinkEnding.Arrow)
            .AddLink(n3, n1, out _, "l3", LinkLineStyle.Thick, LinkEnding.Arrow)
            .AddLink(n1, n3, out _, "l4", LinkLineStyle.Invisible, LinkEnding.Arrow)
            .AddLink(n4, n5, out _, "l5", LinkLineStyle.Solid, LinkEnding.Open)
            .AddLink(n5, n6, out _, "l6", LinkLineStyle.Dotted, LinkEnding.Open)
            .AddLink(n6, n4, out _, "l7", LinkLineStyle.Thick, LinkEnding.Open)
            .AddLink(n4, n6, out _, "l8", LinkLineStyle.Invisible, LinkEnding.Open)
            .AddLink(n7, n8, out _, "l9", LinkLineStyle.Solid, LinkEnding.Circle)
            .AddLink(n8, n9, out _, "l10", LinkLineStyle.Dotted, LinkEnding.Circle)
            .AddLink(n9, n7, out _, "l11", LinkLineStyle.Thick, LinkEnding.Circle)
            .AddLink(n7, n9, out _, "l12", LinkLineStyle.Invisible, LinkEnding.Circle)
            .AddLink(n10, n11, out _, "l13", LinkLineStyle.Solid, LinkEnding.Cross)
            .AddLink(n11, n12, out _, "l14", LinkLineStyle.Dotted, LinkEnding.Cross)
            .AddLink(n12, n10, out _, "l15", LinkLineStyle.Thick, LinkEnding.Cross)
            .AddLink(n10, n12, out _, "l16", LinkLineStyle.Invisible, LinkEnding.Cross)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAllLinkCurveStyles()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddLink(n1, n2, out _, "l1", curveStyle: CurveStyle.Basis)
            .AddLink(n2, n3, out _, "l2", curveStyle: CurveStyle.BumpX)
            .AddLink(n3, n4, out _, "l3", curveStyle: CurveStyle.BumpY)
            .AddLink(n4, n1, out _, "l4", curveStyle: CurveStyle.Cardinal)
            .AddLink(n1, n3, out _, "l5", curveStyle: CurveStyle.CatmullRom)
            .AddLink(n2, n4, out _, "l6", curveStyle: CurveStyle.Linear)
            .AddLink(n3, n1, out _, "l7", curveStyle: CurveStyle.MonotoneX)
            .AddLink(n4, n2, out _, "l8", curveStyle: CurveStyle.MonotoneY)
            .AddLink(n1, n4, out _, "l9", curveStyle: CurveStyle.Natural)
            .AddLink(n2, n1, out _, "l10", curveStyle: CurveStyle.Step)
            .AddLink(n3, n2, out _, "l11", curveStyle: CurveStyle.StepAfter)
            .AddLink(n4, n3, out _, "l12", curveStyle: CurveStyle.StepBefore)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithMultidirectionalLinks()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node _)
            .AddLink(n1, n2, out _, "l1", ending: LinkEnding.Arrow, multidirectional: true)
            .AddLink(n2, n3, out _, "l2", ending: LinkEnding.Open, multidirectional: true)
            .AddLink(n3, n1, out _, "l3", ending: LinkEnding.Circle, multidirectional: true)
            .AddLink(n4, n5, out _, "l4", ending: LinkEnding.Cross, multidirectional: true)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithLinkChains()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node n6)
            .AddLinkChain([n1, n2, n3], [n4, n5, n6], out _, "foo")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithExtraLengthLinks()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddLink(n1, n2, out _, extraLength: 3)
            .AddLink(n2, n3, out _, "foo", extraLength: 3)
            .AddLink(n3, n4, out _, lineStyle: LinkLineStyle.Dotted, extraLength: 3)
            .AddLink(n4, n1, out _, lineStyle: LinkLineStyle.Thick, extraLength: 3)
            .AddLink(n1, n3, out _, lineStyle: LinkLineStyle.Invisible, extraLength: 3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGraphWithSubgraphs()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddLink(n1, n2, out _)
            .AddSubgraph("SG1", out Subgraph sg1, builder => builder
                .AddLink(n2, n3, out _)
                .AddLink(n3, n4, out _)
                .AddSubgraph("SG1.1", out Subgraph _, builder => builder
                    .AddLink(n1, n5, out _)))
            .AddLink(n4, n1, out _)
            .AddSubgraph("SG2", out Subgraph sg2, builder => builder
                .AddNode("N6", out Node n6)
                .AddNode("N7", out Node n7)
                .AddLink(n6, n7, out _), FlowchartOrientation.BottomToTop)
            .AddLink(n1, sg1, out _)
            .AddLink(sg2, n4, out _)
            .AddLink(sg1, sg2, out _)
            .AddLinkChain([n2, sg2], [n1, sg1], out _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNodeClickBindings()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddCallback(n1, "callback", "tooltip")
            .AddCallback(n2, "callback")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNodeHyperlinks()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddHyperlink(n1, "https://example.com")
            .AddHyperlink(n2, "https://example.com", "tooltip", HyperlinkTarget.Blank)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithComments()
    {
        string diagram = Mermaid
            .Flowchart()
            .Comment("This is a comment")
            .AddNode("N1", out _)
            .Comment("This is another comment")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramStyledLinks()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddLink(n1, n2, out Link l1)
            .AddLink(n2, n1, out Link l2)
            .AddLink(n1, n2, out Link l3)
            .AddLink(n2, n1, out Link l4)
            .StyleLinks("stroke: red;", l1, l3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramStyledNodesWithRawCss()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .StyleNodes("fill: red;", n1)
            .StyleNodes("fill: green;", n2, n3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramStyledNodesWithCssClasses()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .DefineCssClass("class1", "fill: red;", out CssClass class1)
            .DefineCssClass("class2", "fill: green;", out CssClass class2)
            .StyleNodes(class1, n1, n3)
            .StyleNodes(class2, n1, n2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramStyledNodesWithPredefinedCssClasses()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .StyleNodesWithPredefinedCssClass("class1", n1, n3)
            .StyleNodesWithPredefinedCssClass("class2", n1, n2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class FlowchartNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildNodesWithDifferentShapes()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out _, NodeShape.Rectangle)
            .AddNode("N2", out _, NodeShape.RoundEdges)
            .AddNode("N3", out _, NodeShape.Stadium)
            .AddNode("N4", out _, NodeShape.Subroutine)
            .AddNode("N5", out _, NodeShape.Cylindrical)
            .AddNode("N6", out _, NodeShape.Circle)
            .AddNode("N7", out _, NodeShape.DoubleCircle)
            .AddNode("N8", out _, NodeShape.Asymmetric)
            .AddNode("N9", out _, NodeShape.Rhombus)
            .AddNode("N10", out _, NodeShape.Hexagon)
            .AddNode("N11", out _, NodeShape.Parallelogram)
            .AddNode("N12", out _, NodeShape.ParallelogramAlt)
            .AddNode("N13", out _, NodeShape.Trapezoid)
            .AddNode("N14", out _, NodeShape.TrapezoidAlt)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildNodesWithDifferentExpandedShapes()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNodeWithExpandedShape("N1", out _, ExpandedNodeShape.NotchRect)
            .AddNodeWithExpandedShape("N2", out _, ExpandedNodeShape.Hourglass)
            .AddNodeWithExpandedShape("N3", out _, ExpandedNodeShape.Bolt)
            .AddNodeWithExpandedShape("N4", out _, ExpandedNodeShape.Brace)
            .AddNodeWithExpandedShape("N5", out _, ExpandedNodeShape.BraceR)
            .AddNodeWithExpandedShape("N6", out _, ExpandedNodeShape.Braces)
            .AddNodeWithExpandedShape("N7", out _, ExpandedNodeShape.LeanR)
            .AddNodeWithExpandedShape("N8", out _, ExpandedNodeShape.LeanL)
            .AddNodeWithExpandedShape("N9", out _, ExpandedNodeShape.Cyl)
            .AddNodeWithExpandedShape("N10", out _, ExpandedNodeShape.Diam)
            .AddNodeWithExpandedShape("N11", out _, ExpandedNodeShape.Delay)
            .AddNodeWithExpandedShape("N12", out _, ExpandedNodeShape.HCyl)
            .AddNodeWithExpandedShape("N13", out _, ExpandedNodeShape.LinCyl)
            .AddNodeWithExpandedShape("N14", out _, ExpandedNodeShape.CurvTrap)
            .AddNodeWithExpandedShape("N15", out _, ExpandedNodeShape.DivRect)
            .AddNodeWithExpandedShape("N16", out _, ExpandedNodeShape.Doc)
            .AddNodeWithExpandedShape("N17", out _, ExpandedNodeShape.Rounded)
            .AddNodeWithExpandedShape("N18", out _, ExpandedNodeShape.Tri)
            .AddNodeWithExpandedShape("N19", out _, ExpandedNodeShape.Fork)
            .AddNodeWithExpandedShape("N20", out _, ExpandedNodeShape.WinPane)
            .AddNodeWithExpandedShape("N21", out _, ExpandedNodeShape.FCirc)
            .AddNodeWithExpandedShape("N22", out _, ExpandedNodeShape.LinDoc)
            .AddNodeWithExpandedShape("N23", out _, ExpandedNodeShape.LinRect)
            .AddNodeWithExpandedShape("N24", out _, ExpandedNodeShape.NotchPent)
            .AddNodeWithExpandedShape("N25", out _, ExpandedNodeShape.FlipTri)
            .AddNodeWithExpandedShape("N26", out _, ExpandedNodeShape.SlRect)
            .AddNodeWithExpandedShape("N27", out _, ExpandedNodeShape.TrapT)
            .AddNodeWithExpandedShape("N28", out _, ExpandedNodeShape.Docs)
            .AddNodeWithExpandedShape("N29", out _, ExpandedNodeShape.StRect)
            .AddNodeWithExpandedShape("N30", out _, ExpandedNodeShape.Odd)
            .AddNodeWithExpandedShape("N31", out _, ExpandedNodeShape.Flag)
            .AddNodeWithExpandedShape("N32", out _, ExpandedNodeShape.Hex)
            .AddNodeWithExpandedShape("N33", out _, ExpandedNodeShape.TrapB)
            .AddNodeWithExpandedShape("N34", out _, ExpandedNodeShape.Rect)
            .AddNodeWithExpandedShape("N35", out _, ExpandedNodeShape.Circle)
            .AddNodeWithExpandedShape("N36", out _, ExpandedNodeShape.SmCirc)
            .AddNodeWithExpandedShape("N37", out _, ExpandedNodeShape.DblCirc)
            .AddNodeWithExpandedShape("N38", out _, ExpandedNodeShape.FrCirc)
            .AddNodeWithExpandedShape("N39", out _, ExpandedNodeShape.BowRect)
            .AddNodeWithExpandedShape("N40", out _, ExpandedNodeShape.FrRect)
            .AddNodeWithExpandedShape("N41", out _, ExpandedNodeShape.CrossCirc)
            .AddNodeWithExpandedShape("N42", out _, ExpandedNodeShape.TagDoc)
            .AddNodeWithExpandedShape("N43", out _, ExpandedNodeShape.TagRect)
            .AddNodeWithExpandedShape("N44", out _, ExpandedNodeShape.Stadium)
            .AddNodeWithExpandedShape("N45", out _, ExpandedNodeShape.Text)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildMarkdownNode()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddMarkdownNode("N1", out _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithDifferentOrientations()
    {
        string diagram1 = Mermaid
            .Flowchart(orientation: FlowchartOrientation.RightToLeft, options: _options)
            .Build();

        string diagram2 = Mermaid
            .Flowchart(orientation: FlowchartOrientation.LeftToRight, options: _options)
            .Build();

        string diagram3 = Mermaid
            .Flowchart(orientation: FlowchartOrientation.BottomToTop, options: _options)
            .Build();

        string diagram4 = Mermaid
            .Flowchart(orientation: FlowchartOrientation.TopToBottom, options: _options)
            .Build();

        var diagram1Result = await toolingFixture.ValidateDiagramAsync(diagram1);

        Assert.True(diagram1Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram1, diagram1Result));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagram1Result));

        var diagram2Result = await toolingFixture.ValidateDiagramAsync(diagram2);

        Assert.True(diagram2Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram2, diagram2Result));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagram2Result));

        var diagram3Result = await toolingFixture.ValidateDiagramAsync(diagram3);

        Assert.True(diagram3Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram3, diagram3Result));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagram3Result));

        var diagram4Result = await toolingFixture.ValidateDiagramAsync(diagram4);

        Assert.True(diagram4Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram4, diagram4Result));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagram4Result));
    }

    [Fact]
    public async Task CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddLink(n1, n2, out _, "some text")
            .AddLink(n2, n3, out _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAllLinkStyles()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node n6)
            .AddNode("N7", out Node n7)
            .AddNode("N8", out Node n8)
            .AddNode("N9", out Node n9)
            .AddNode("N10", out Node n10)
            .AddNode("N11", out Node n11)
            .AddNode("N12", out Node n12)
            .AddLink(n1, n2, out _, "l1", LinkLineStyle.Solid, LinkEnding.Arrow)
            .AddLink(n2, n3, out _, "l2", LinkLineStyle.Dotted, LinkEnding.Arrow)
            .AddLink(n3, n1, out _, "l3", LinkLineStyle.Thick, LinkEnding.Arrow)
            .AddLink(n1, n3, out _, "l4", LinkLineStyle.Invisible, LinkEnding.Arrow)
            .AddLink(n4, n5, out _, "l5", LinkLineStyle.Solid, LinkEnding.Open)
            .AddLink(n5, n6, out _, "l6", LinkLineStyle.Dotted, LinkEnding.Open)
            .AddLink(n6, n4, out _, "l7", LinkLineStyle.Thick, LinkEnding.Open)
            .AddLink(n4, n6, out _, "l8", LinkLineStyle.Invisible, LinkEnding.Open)
            .AddLink(n7, n8, out _, "l9", LinkLineStyle.Solid, LinkEnding.Circle)
            .AddLink(n8, n9, out _, "l10", LinkLineStyle.Dotted, LinkEnding.Circle)
            .AddLink(n9, n7, out _, "l11", LinkLineStyle.Thick, LinkEnding.Circle)
            .AddLink(n7, n9, out _, "l12", LinkLineStyle.Invisible, LinkEnding.Circle)
            .AddLink(n10, n11, out _, "l13", LinkLineStyle.Solid, LinkEnding.Cross)
            .AddLink(n11, n12, out _, "l14", LinkLineStyle.Dotted, LinkEnding.Cross)
            .AddLink(n12, n10, out _, "l15", LinkLineStyle.Thick, LinkEnding.Cross)
            .AddLink(n10, n12, out _, "l16", LinkLineStyle.Invisible, LinkEnding.Cross)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAllLinkCurveStyles()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddLink(n1, n2, out _, "l1", curveStyle: CurveStyle.Basis)
            .AddLink(n2, n3, out _, "l2", curveStyle: CurveStyle.BumpX)
            .AddLink(n3, n4, out _, "l3", curveStyle: CurveStyle.BumpY)
            .AddLink(n4, n1, out _, "l4", curveStyle: CurveStyle.Cardinal)
            .AddLink(n1, n3, out _, "l5", curveStyle: CurveStyle.CatmullRom)
            .AddLink(n2, n4, out _, "l6", curveStyle: CurveStyle.Linear)
            .AddLink(n3, n1, out _, "l7", curveStyle: CurveStyle.MonotoneX)
            .AddLink(n4, n2, out _, "l8", curveStyle: CurveStyle.MonotoneY)
            .AddLink(n1, n4, out _, "l9", curveStyle: CurveStyle.Natural)
            .AddLink(n2, n1, out _, "l10", curveStyle: CurveStyle.Step)
            .AddLink(n3, n2, out _, "l11", curveStyle: CurveStyle.StepAfter)
            .AddLink(n4, n3, out _, "l12", curveStyle: CurveStyle.StepBefore)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithMultidirectionalLinks()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node _)
            .AddLink(n1, n2, out _, "l1", ending: LinkEnding.Arrow, multidirectional: true)
            .AddLink(n2, n3, out _, "l2", ending: LinkEnding.Open, multidirectional: true)
            .AddLink(n3, n1, out _, "l3", ending: LinkEnding.Circle, multidirectional: true)
            .AddLink(n4, n5, out _, "l4", ending: LinkEnding.Cross, multidirectional: true)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithLinkChains()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node n6)
            .AddLinkChain([n1, n2, n3], [n4, n5, n6], out _, "foo")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithExtraLengthLinks()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddLink(n1, n2, out _, extraLength: 3)
            .AddLink(n2, n3, out _, "foo", extraLength: 3)
            .AddLink(n3, n4, out _, lineStyle: LinkLineStyle.Dotted, extraLength: 3)
            .AddLink(n4, n1, out _, lineStyle: LinkLineStyle.Thick, extraLength: 3)
            .AddLink(n1, n3, out _, lineStyle: LinkLineStyle.Invisible, extraLength: 3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGraphWithSubgraphs()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddLink(n1, n2, out _)
            .AddSubgraph("SG1", out Subgraph sg1, builder => builder
                .AddLink(n2, n3, out _)
                .AddLink(n3, n4, out _)
                .AddSubgraph("SG1.1", out Subgraph _, builder => builder
                    .AddLink(n1, n5, out _)))
            .AddLink(n4, n1, out _)
            .AddSubgraph("SG2", out Subgraph sg2, builder => builder
                .AddNode("N6", out Node n6)
                .AddNode("N7", out Node n7)
                .AddLink(n6, n7, out _), FlowchartOrientation.BottomToTop)
            .AddLink(n1, sg1, out _)
            .AddLink(sg2, n4, out _)
            .AddLink(sg1, sg2, out _)
            .AddLinkChain([n2, sg2], [n1, sg1], out _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNodeClickBindings()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddCallback(n1, "callback", "tooltip")
            .AddCallback(n2, "callback")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNodeHyperlinks()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddHyperlink(n1, "https://example.com")
            .AddHyperlink(n2, "https://example.com", "tooltip", HyperlinkTarget.Blank)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithComments()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .Comment("This is a comment")
            .AddNode("N1", out _)
            .Comment("This is another comment")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramStyledLinks()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddLink(n1, n2, out Link l1)
            .AddLink(n2, n1, out Link l2)
            .AddLink(n1, n2, out Link l3)
            .AddLink(n2, n1, out Link l4)
            .StyleLinks("stroke: red;", l1, l3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramStyledNodesWithRawCss()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .StyleNodes("fill: red;", n1)
            .StyleNodes("fill: green;", n2, n3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramStyledNodesWithCssClasses()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .DefineCssClass("class1", "fill: red;", out CssClass class1)
            .DefineCssClass("class2", "fill: green;", out CssClass class2)
            .StyleNodes(class1, n1, n3)
            .StyleNodes(class2, n1, n2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("flowchart-v2", toolingFixture.GetDiagramType(diagramResult));
    }
}
