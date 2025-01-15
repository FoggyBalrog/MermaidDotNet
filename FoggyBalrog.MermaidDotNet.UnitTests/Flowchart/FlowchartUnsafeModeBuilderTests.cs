using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.Flowchart;

public class FlowchartUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .Unsafe
            .Flowchart()
            .Build();

        Assert.Equal("flowchart TB", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildNodesWithDifferentShapes()
    {
        string diagram = Mermaid
            .Unsafe
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
    public void CanBuildNodesWithDifferentExpandedShapes()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"flowchart TB
    id1@{ shape: notch-rect, label: ""N1"" }
    id2@{ shape: hourglass, label: ""N2"" }
    id3@{ shape: bolt, label: ""N3"" }
    id4@{ shape: brace, label: ""N4"" }
    id5@{ shape: brace-r, label: ""N5"" }
    id6@{ shape: braces, label: ""N6"" }
    id7@{ shape: lean-r, label: ""N7"" }
    id8@{ shape: lean-l, label: ""N8"" }
    id9@{ shape: cyl, label: ""N9"" }
    id10@{ shape: diam, label: ""N10"" }
    id11@{ shape: delay, label: ""N11"" }
    id12@{ shape: h-cyl, label: ""N12"" }
    id13@{ shape: lin-cyl, label: ""N13"" }
    id14@{ shape: curv-trap, label: ""N14"" }
    id15@{ shape: div-rect, label: ""N15"" }
    id16@{ shape: doc, label: ""N16"" }
    id17@{ shape: rounded, label: ""N17"" }
    id18@{ shape: tri, label: ""N18"" }
    id19@{ shape: fork, label: ""N19"" }
    id20@{ shape: win-pane, label: ""N20"" }
    id21@{ shape: f-circ, label: ""N21"" }
    id22@{ shape: lin-doc, label: ""N22"" }
    id23@{ shape: lin-rect, label: ""N23"" }
    id24@{ shape: notch-pent, label: ""N24"" }
    id25@{ shape: flip-tri, label: ""N25"" }
    id26@{ shape: sl-rect, label: ""N26"" }
    id27@{ shape: trap-t, label: ""N27"" }
    id28@{ shape: docs, label: ""N28"" }
    id29@{ shape: st-rect, label: ""N29"" }
    id30@{ shape: odd, label: ""N30"" }
    id31@{ shape: flag, label: ""N31"" }
    id32@{ shape: hex, label: ""N32"" }
    id33@{ shape: trap-b, label: ""N33"" }
    id34@{ shape: rect, label: ""N34"" }
    id35@{ shape: circle, label: ""N35"" }
    id36@{ shape: sm-circ, label: ""N36"" }
    id37@{ shape: dbl-circ, label: ""N37"" }
    id38@{ shape: fr-circ, label: ""N38"" }
    id39@{ shape: bow-rect, label: ""N39"" }
    id40@{ shape: fr-rect, label: ""N40"" }
    id41@{ shape: cross-circ, label: ""N41"" }
    id42@{ shape: tag-doc, label: ""N42"" }
    id43@{ shape: tag-rect, label: ""N43"" }
    id44@{ shape: stadium, label: ""N44"" }
    id45@{ shape: text, label: ""N45"" }", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildMarkdownNode()
    {
        string diagram = Mermaid
            .Unsafe
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
            .Unsafe
            .Flowchart(orientation: FlowchartOrientation.RightToLeft)
            .Build();

        string diagram2 = Mermaid
            .Unsafe
            .Flowchart(orientation: FlowchartOrientation.LeftToRight)
            .Build();

        string diagram3 = Mermaid
            .Unsafe
            .Flowchart(orientation: FlowchartOrientation.BottomToTop)
            .Build();

        string diagram4 = Mermaid
            .Unsafe
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
            .Unsafe
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddLink(n1, n2, out _, "some text")
            .AddLink(n2, n3, out _)
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
            .Unsafe
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
            .Unsafe
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
            .Unsafe
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddNode("N5", out Node n5)
            .AddNode("N6", out Node n6)
            .AddLinkChain([n1, n2, n3], [n4, n5, n6], out _, "foo")
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
            .Unsafe
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
            .Unsafe
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
            .Unsafe
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
            .Unsafe
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
            .Unsafe
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

    [Fact]
    public void CanBuildDiagramStyledLinks()
    {
        string diagram = Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddLink(n1, n2, out Link l1)
            .AddLink(n2, n1, out Link l2)
            .AddLink(n1, n2, out Link l3)
            .AddLink(n2, n1, out Link l4)
            .StyleLinks("stroke: red;", l1, l3)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id1 --> id2
    id2 --> id1
    id1 --> id2
    id2 --> id1
    linkStyle 0,2 stroke: red;", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramStyledNodesWithRawCss()
    {
        string diagram = Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .StyleNodes("fill: red;", n1)
            .StyleNodes("fill: green;", n2, n3)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id3[""N3""]
    style id1 fill: red;
    style id2 fill: green;
    style id3 fill: green;", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramStyledNodesWithCssClasses()
    {
        string diagram = Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .DefineCssClass("class1", "fill: red;", out CssClass class1)
            .DefineCssClass("class2", "fill: green;", out CssClass class2)
            .StyleNodes(class1, n1, n3)
            .StyleNodes(class2, n1, n2)
            .Build();

        Assert.Equal(@"flowchart TB
    id1[""N1""]
    id2[""N2""]
    id3[""N3""]
    classDef class1 fill: red;
    classDef class2 fill: green;
    class id1,id3 class1
    class id1,id2 class2", diagram, ignoreLineEndingDifferences: true);
    }
}
