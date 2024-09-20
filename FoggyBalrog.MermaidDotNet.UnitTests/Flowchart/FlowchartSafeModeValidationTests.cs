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
                .AddLink(from, to, "text");
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
                .AddLink(from, to, "text");
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
                .AddLink(from, to, " ");
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
                .AddLink(from, to, "text", extraLength: -1);
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
                .AddLinkChain([from1, from2, from3], [to1, to2, to3], "text");

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
                .AddLinkChain([from1, from2, from3], [to1, to2, to3], "text");

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
                .AddLinkChain([from1, from2, from3], [to1, to2, to3], " ");

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
                .AddLinkChain([from1, from2, from3], [to1, to2, to3], "text", extraLength: -1);

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
}
