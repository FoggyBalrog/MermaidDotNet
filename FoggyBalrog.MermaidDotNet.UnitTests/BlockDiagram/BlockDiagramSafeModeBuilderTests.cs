using FoggyBalrog.MermaidDotNet.BlockDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.BlockDiagram;

public class BlockDiagramSafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .BlockDiagram()
            .Build();

        Assert.Equal("block", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .BlockDiagram()
            .AddBlock("foo", out var foo)
            .AddSpace()
            .AddBlock("bar", out var bar)
            .AddSpace()
            .AddBlock("baz", out var baz)
            .AddLink(foo, bar, "qux")
            .AddLink(bar, baz)
            .Build();

        Assert.Equal(@"block
    b0[""foo""]
    space
    b1[""bar""]
    space
    b2[""baz""]
    b0 --""qux""--> b1
    b1 --> b2", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildComplexDiagram()
    {
        Block? e = null;

        string diagram = Mermaid
            .BlockDiagram(columns: 4)
            .AddBlock("A", out var a)
            .AddBlock("B", out var b)
            .AddBlock("C", out var c)
            .AddBlock("D", out var d)
            .AddCompositeBlock(composite =>
            {
                composite
                    .AddBlock("E", out e)
                    .AddBlock("F", out var f)
                    .AddCompositeBlock(nested =>
                    {
                        nested
                            .AddBlock("G", out var g)
                            .AddBlock("H", out var h)
                            .AddLink(g, h, "links G to H");
                    }, columns: 1)
                    .AddLink(e, f, "links E to F")
                    .AddLink(c, f, "links C to F");
            }, columns: 2, width: 5)
            .AddBlock("I", out var i)
            .AddLink(a, b)
            .AddLink(e!, i)
            .Build();

        Assert.Equal(@"block
    columns 4
    b0[""A""]
    b1[""B""]
    b2[""C""]
    b3[""D""]
    block:composite0:5
        columns 2
        b4[""E""]
        b5[""F""]
        block
            columns 1
            b6[""G""]
            b7[""H""]
            b6 --""links G to H""--> b7
        end
        b4 --""links E to F""--> b5
        b2 --""links C to F""--> b5
    end
    b8[""I""]
    b0 --> b1
    b4 --> b8", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithAllShapes()
    {
        string diagram = Mermaid
            .BlockDiagram()
            .AddBlock("A", out var a, shape: BlockShape.Rectangle)
            .AddBlock("B", out var b, shape: BlockShape.RoundEdges)
            .AddBlock("C", out var c, shape: BlockShape.Stadium)
            .AddBlock("D", out var d, shape: BlockShape.Subroutine)
            .AddBlock("E", out var e, shape: BlockShape.Cylindrical)
            .AddBlock("F", out var f, shape: BlockShape.Circle)
            .AddBlock("G", out var g, shape: BlockShape.DoubleCircle)
            .AddBlock("H", out var h, shape: BlockShape.Asymmetric)
            .AddBlock("I", out var i, shape: BlockShape.Rhombus)
            .AddBlock("J", out var j, shape: BlockShape.Hexagon)
            .AddBlock("K", out var k, shape: BlockShape.Parallelogram)
            .AddBlock("L", out var l, shape: BlockShape.ParallelogramAlt)
            .AddBlock("M", out var m, shape: BlockShape.Trapezoid)
            .AddBlock("N", out var n, shape: BlockShape.TrapezoidAlt)
            .AddBlock("O", out var o, shape: BlockShape.RightArrow)
            .AddBlock("P", out var p, shape: BlockShape.LeftArrow)
            .AddBlock("Q", out var q, shape: BlockShape.UpArrow)
            .AddBlock("R", out var r, shape: BlockShape.DownArrow)
            .AddBlock("S", out var s, shape: BlockShape.XArrow)
            .AddBlock("T", out var t, shape: BlockShape.YArrow)
            .Build();

        Assert.Equal(@"block
    b0[""A""]
    b1(""B"")
    b2([""C""])
    b3[[""D""]]
    b4[(""E"")]
    b5((""F""))
    b6(((""G"")))
    b7>""H""]
    b8{""I""}
    b9{{""J""}}
    b10[/""K""/]
    b11[\""L""\]
    b12[/""M""\]
    b13[\""N""/]
    b14<[""O""]>(right)
    b15<[""P""]>(left)
    b16<[""Q""]>(up)
    b17<[""R""]>(down)
    b18<[""S""]>(x)
    b19<[""T""]>(y)", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithStyles()
    {
        string diagram = Mermaid
            .BlockDiagram()
            .AddBlock("A", out var a)
            .AddBlock("B", out var b)
            .StyleBlock(a, "fill:#f96,stroke:#333,stroke-width:4px")
            .StyleBlock(b, "fill:#bbf,stroke:#f66,stroke-width:2px")
            .Build();

        Assert.Equal(@"block
    b0[""A""]
    b1[""B""]
    style b0 fill:#f96,stroke:#333,stroke-width:4px
    style b1 fill:#bbf,stroke:#f66,stroke-width:2px", diagram, ignoreLineEndingDifferences: true);
    }
}
