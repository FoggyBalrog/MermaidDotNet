using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.Flowchart;

public class FlowchartSafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .Flowchart()
            .Build();

        Assert.Equal("flowchart TB", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildNodesWithDifferentShapes()
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

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2(""N2"")
    id3([""N3""])
    id4[[""N4""]]
    id5[(""N5"")]
    id6((""N6""))
    id7(((""N7"")))
    id8>""N8""]
    id9{""N9""}
    id10{{""N10""}}
    id11[/""N11""/]
    id12[\""N12""\]
    id13[/""N13""\]
    id14[\""N14""/]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildMarkdownNode()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddMarkdownNode("N1", out _)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""`N1`""]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithDifferentOrientations()
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

        Assert.Equal("flowchart RL", diagram1, ignoreLineEndingDifferences: true);
        Assert.Equal("flowchart LR", diagram2, ignoreLineEndingDifferences: true);
        Assert.Equal("flowchart BT", diagram3, ignoreLineEndingDifferences: true);
        Assert.Equal("flowchart TB", diagram4, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddLink(n1, n2, "some text")
            .AddLink(n2, n3)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id3[""N3""]
    id1 -->|""some text""| id2
    id2 --> id3", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithAllLinkStyles()
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
            .AddLink(n1, n2, "l1", LinkLineStyle.Solid, LinkEnding.Arrow)
            .AddLink(n2, n3, "l2", LinkLineStyle.Dotted, LinkEnding.Arrow)
            .AddLink(n3, n1, "l3", LinkLineStyle.Thick, LinkEnding.Arrow)
            .AddLink(n1, n3, "l4", LinkLineStyle.Invisible, LinkEnding.Arrow)
            .AddLink(n4, n5, "l5", LinkLineStyle.Solid, LinkEnding.Open)
            .AddLink(n5, n6, "l6", LinkLineStyle.Dotted, LinkEnding.Open)
            .AddLink(n6, n4, "l7", LinkLineStyle.Thick, LinkEnding.Open)
            .AddLink(n4, n6, "l8", LinkLineStyle.Invisible, LinkEnding.Open)
            .AddLink(n7, n8, "l9", LinkLineStyle.Solid, LinkEnding.Circle)
            .AddLink(n8, n9, "l10", LinkLineStyle.Dotted, LinkEnding.Circle)
            .AddLink(n9, n7, "l11", LinkLineStyle.Thick, LinkEnding.Circle)
            .AddLink(n7, n9, "l12", LinkLineStyle.Invisible, LinkEnding.Circle)
            .AddLink(n10, n11, "l13", LinkLineStyle.Solid, LinkEnding.Cross)
            .AddLink(n11, n12, "l14", LinkLineStyle.Dotted, LinkEnding.Cross)
            .AddLink(n12, n10, "l15", LinkLineStyle.Thick, LinkEnding.Cross)
            .AddLink(n10, n12, "l16", LinkLineStyle.Invisible, LinkEnding.Cross)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id3[""N3""]
    id4[""N4""]
    id5[""N5""]
    id6[""N6""]
    id7[""N7""]
    id8[""N8""]
    id9[""N9""]
    id10[""N10""]
    id11[""N11""]
    id12[""N12""]
    id1 -->|""l1""| id2
    id2 -.->|""l2""| id3
    id3 ==>|""l3""| id1
    id1 ~~~|""l4""| id3
    id4 ---|""l5""| id5
    id5 -.-|""l6""| id6
    id6 ===|""l7""| id4
    id4 ~~~|""l8""| id6
    id7 --o|""l9""| id8
    id8 -.-o|""l10""| id9
    id9 ==o|""l11""| id7
    id7 ~~~|""l12""| id9
    id10 --x|""l13""| id11
    id11 -.-x|""l14""| id12
    id12 ==x|""l15""| id10
    id10 ~~~|""l16""| id12", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithMultidirectionalLinks()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node _)
            .AddLink(n1, n2, "l1", ending: LinkEnding.Arrow, multidirectional: true)
            .AddLink(n2, n3, "l2", ending: LinkEnding.Open, multidirectional: true)
            .AddLink(n3, n1, "l3", ending: LinkEnding.Circle, multidirectional: true)
            .AddLink(n4, n5, "l4", ending: LinkEnding.Cross, multidirectional: true)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id3[""N3""]
    id4[""N4""]
    id5[""N5""]
    id6[""N6""]
    id1 <-->|""l1""| id2
    id2 ---|""l2""| id3
    id3 o--o|""l3""| id1
    id4 x--x|""l4""| id5", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithLinkChains()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node n6)
            .AddLinkChain([n1, n2, n3], [n4, n5, n6], "foo")
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id3[""N3""]
    id4[""N4""]
    id5[""N5""]
    id6[""N6""]
    id1 & id2 & id3 -->|""foo""| id4 & id5 & id6", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithExtraLengthLinks()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddLink(n1, n2, extraLength: 3)
            .AddLink(n2, n3, "foo", extraLength: 3)
            .AddLink(n3, n4, lineStyle: LinkLineStyle.Dotted, extraLength: 3)
            .AddLink(n4, n1, lineStyle: LinkLineStyle.Thick, extraLength: 3)
            .AddLink(n1, n3, lineStyle: LinkLineStyle.Invisible, extraLength: 3)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id3[""N3""]
    id4[""N4""]
    id1 -----> id2
    id2 ----->|""foo""| id3
    id3 -....-> id4
    id4 =====> id1
    id1 ~~~~~~ id3", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGraphWithSubgraphs()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddLink(n1, n2)
            .AddSubgraph("SG1", out Subgraph sg1, builder => builder
                .AddLink(n2, n3)
                .AddLink(n3, n4)
                .AddSubgraph("SG1.1", out Subgraph _, builder => builder
                    .AddLink(n1, n5)))
            .AddLink(n4, n1)
            .AddSubgraph("SG2", out Subgraph sg2, builder => builder
                .AddNode("N6", out Node n6)
                .AddNode("N7", out Node n7)
                .AddLink(n6, n7), FlowchartOrientation.BottomToTop)
            .AddLink(n1, sg1)
            .AddLink(sg2, n4)
            .AddLink(sg1, sg2)
            .AddLinkChain([n2, sg2], [n1, sg1])
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id3[""N3""]
    id4[""N4""]
    id5[""N5""]
    id1 --> id2
    subgraph sub7 [SG1]
    id2 --> id3
    id3 --> id4
    subgraph sub10 [SG1.1]
    id1 --> id5
    end
    end
    id4 --> id1
    subgraph sub15 [SG2]
    direction BT
    id16[""N6""]
    id17[""N7""]
    id16 --> id17
    end
    id1 --> sub7
    sub15 --> id4
    sub7 --> sub15
    id2 & sub15 --> id1 & sub7", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithNodeClickBindings()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddCallback(n1, "callback", "tooltip")
            .AddCallback(n2, "callback")
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    click id1 callback ""tooltip""
    id2[""N2""]
    click id2 callback", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithNodeHyperlinks()
    {
        string diagram = Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddHyperlink(n1, "https://example.com")
            .AddHyperlink(n2, "https://example.com", "tooltip", HyperlinkTarget.Blank)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    click id1 ""https://example.com"" _self
    id2[""N2""]
    click id2 ""https://example.com"" ""tooltip"" _blank", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithComments()
    {
        string diagram = Mermaid
            .Flowchart()
            .Comment("This is a comment")
            .AddNode("N1", out _)
            .Comment("This is another comment")
            .Build();

        Assert.Equal(@"flowchart TB
    %% This is a comment
    id2[""N1""]
    %% This is another comment", diagram, ignoreLineEndingDifferences: true);
    }
}
