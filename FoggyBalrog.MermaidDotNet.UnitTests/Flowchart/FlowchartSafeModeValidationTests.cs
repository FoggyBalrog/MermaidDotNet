using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.Flowchart;

public class FlowchartSafeModeValidationTests
{
    [Fact]
    public void AddNode_ThrowsIfTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNodeWithExpandedShape_ThrowsIfTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNodeWithExpandedShape(" ", out var _, ExpandedNodeShape.NotchRect);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddMarkdownNode_ThrowsIfMarkdownIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddMarkdownNode(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddLink_ThrowsIfFromIsForeign()
    {
        Mermaid
            .Flowchart()
            .AddNode("from", out var from);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("to", out var to)
                .AddLink(from, to, out _, "text");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddLink_ThrowsIfToIsForeign()
    {
        Mermaid
            .Flowchart()
            .AddNode("to", out var to);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("from", out var from)
                .AddLink(from, to, out _, "text");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddLink_ThrowsIfTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("from", out var from)
                .AddNode("to", out var to)
                .AddLink(from, to, out _, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddLink_ThrowsIfExtraLengthIsStrictlyNegative()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("from", out var from)
                .AddNode("to", out var to)
                .AddLink(from, to, out _, "text", extraLength: -1);
        });

        Assert.Equal(MermaidExceptionReason.StrictlyNegative, exception.Reason);
    }

    [Fact]
    public void AddLinkChain_ThrowsIfFromIsForeign()
    {
        Mermaid
            .Flowchart()
            .AddNode("from2", out var from2);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("from1", out var from1)
                .AddNode("from3", out var from3)
                .AddNode("to1", out var to1)
                .AddNode("to2", out var to2)
                .AddNode("to3", out var to3)
                .AddLinkChain([from1, from2, from3], [to1, to2, to3], out _, "text");

        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddLinkChain_ThrowsIfToIsForeign()
    {
        Mermaid
            .Flowchart()
            .AddNode("to2", out var to2);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("from1", out var from1)
                .AddNode("from2", out var from2)
                .AddNode("from3", out var from3)
                .AddNode("to1", out var to1)
                .AddNode("to3", out var to3)
                .AddLinkChain([from1, from2, from3], [to1, to2, to3], out _, "text");

        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddLinkChain_ThrowsIfTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("from1", out var from1)
                .AddNode("from2", out var from2)
                .AddNode("from3", out var from3)
                .AddNode("to1", out var to1)
                .AddNode("to2", out var to2)
                .AddNode("to3", out var to3)
                .AddLinkChain([from1, from2, from3], [to1, to2, to3], out _, " ");

        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddLinkChain_ThrowsIfExtraLengthIsStrictlyNegative()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("from1", out var from1)
                .AddNode("from2", out var from2)
                .AddNode("from3", out var from3)
                .AddNode("to1", out var to1)
                .AddNode("to2", out var to2)
                .AddNode("to3", out var to3)
                .AddLinkChain([from1, from2, from3], [to1, to2, to3], out _, "text", extraLength: -1);

        });

        Assert.Equal(MermaidExceptionReason.StrictlyNegative, exception.Reason);
    }

    [Fact]
    public void AddCallback_ThrowsIfNodeIsForeign()
    {
        Mermaid
            .Flowchart()
            .AddNode("node", out var node);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddCallback(node, "functionName");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddCallback_ThrowsIfFunctionNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("node", out var node)
                .AddCallback(node, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddCallback_ThrowsIfTooltipIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("node", out var node)
                .AddCallback(node, "functionName", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddHyperlink_ThrowsIfNodeIsForeign()
    {
        Mermaid
            .Flowchart()
            .AddNode("node", out var node);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddHyperlink(node, "uri");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddHyperlink_ThrowsIfUriIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("node", out var node)
                .AddHyperlink(node, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddHyperlink_ThrowsIfTooltipIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("node", out var node)
                .AddHyperlink(node, "uri", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddSubgraph_ThrowsIfTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddSubgraph(" ", out var _, builder => { });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Comment_ThrowsIfTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .Comment(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void StyleLinks_ThrowsIfCssIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("N1", out Node n1)
                .AddNode("N2", out Node n2)
                .AddLink(n1, n2, out Link l1)
                .StyleLinks(" ", l1);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void StyleLinks_ThrowsIfNoLinksAreProvided()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .StyleLinks("css");
        });

        Assert.Equal(MermaidExceptionReason.EmptyCollection, exception.Reason);
    }

    [Fact]
    public void StyleLinks_ThrowsIfForeignLinkIsProvided()
    {
        Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddLink(n1, n2, out Link l1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("N3", out Node n3)
                .AddNode("N4", out Node n4)
                .AddLink(n3, n4, out Link l2)
                .StyleLinks("foo", l1, l2);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void StyleNodes_ThrowsIfCssIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("N1", out Node n1)
                .StyleNodes(" ", n1);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void StyleNodes_ThrowsIfCssClassIsForeign()
    {
        Mermaid
            .Flowchart()
            .DefineCssClass("foo", "bar", out CssClass cssClass);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("N1", out Node n1)
                .StyleNodes(cssClass, n1);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void StyleNodes_ThrowsIfNoNodesAreProvided()
    {
        var exception1 = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .StyleNodes("css");
        });

        var exception2 = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .DefineCssClass("foo", "bar", out CssClass cssClass)
                .StyleNodes(cssClass);
        });

        Assert.Equal(MermaidExceptionReason.EmptyCollection, exception1.Reason);
        Assert.Equal(MermaidExceptionReason.EmptyCollection, exception2.Reason);
    }

    [Fact]
    public void StyleNodes_ThrowsIfForeignNodeIsProvided()
    {
        Mermaid
            .Flowchart()
            .AddNode("N1", out Node n1);

        var exception1 = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .AddNode("N2", out Node n2)
                .StyleNodes("foo", n1, n2);
        });

        var exception2 = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .DefineCssClass("foo", "bar", out CssClass cssClass)
                .AddNode("N2", out Node n2)
                .StyleNodes(cssClass, n1, n2);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception1.Reason);
        Assert.Equal(MermaidExceptionReason.ForeignItem, exception2.Reason);
    }

    [Fact]
    public void DefineCssClass_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .DefineCssClass(" ", "css", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void DefineCssClass_ThrowsIfCssIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .Flowchart()
                .DefineCssClass("foo", " ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}
