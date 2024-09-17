namespace FoggyBalrog.MermaidDotNet.UnitTests.Flowchart;

public class FlowchartUnsafeModeValidationTests
{
    [Fact]
    public void AddNode_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddMarkdownNode_DoesNotThrowIfMarkdownIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddMarkdownNode(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfFromIsForeign()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from", out var from);

        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("to", out var to)
            .AddLink(from, to, "text")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfToIsForeign()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("to", out var to);

        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from", out var from)
            .AddLink(from, to, "text")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from", out var from)
            .AddNode("to", out var to)
            .AddLink(from, to, " ")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfExtraLengthIsStrictlyNegative()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from", out var from)
            .AddNode("to", out var to)
            .AddLink(from, to, "text", extraLength: -1)
            .Build();
    }

    [Fact]
    public void AddLinkChain_DoesNotThrowIfFromIsForeign()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from2", out var from2);

        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from1", out var from1)
            .AddNode("from3", out var from3)
            .AddNode("to1", out var to1)
            .AddNode("to2", out var to2)
            .AddNode("to3", out var to3)
            .AddLinkChain([from1, from2, from3], [to1, to2, to3], "text")
            .Build();
    }

    [Fact]
    public void AddLinkChain_DoesNotThrowIfToIsForeign()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("to2", out var to2);

        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from1", out var from1)
            .AddNode("from2", out var from2)
            .AddNode("from3", out var from3)
            .AddNode("to1", out var to1)
            .AddNode("to3", out var to3)
            .AddLinkChain([from1, from2, from3], [to1, to2, to3], "text")
            .Build();
    }

    [Fact]
    public void AddLinkChain_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from1", out var from1)
            .AddNode("from2", out var from2)
            .AddNode("from3", out var from3)
            .AddNode("to1", out var to1)
            .AddNode("to2", out var to2)
            .AddNode("to3", out var to3)
            .AddLinkChain([from1, from2, from3], [to1, to2, to3], " ")
            .Build();
    }

    [Fact]
    public void AddLinkChain_DoesNotThrowIfExtraLengthIsStrictlyNegative()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("from1", out var from1)
            .AddNode("from2", out var from2)
            .AddNode("from3", out var from3)
            .AddNode("to1", out var to1)
            .AddNode("to2", out var to2)
            .AddNode("to3", out var to3)
            .AddLinkChain([from1, from2, from3], [to1, to2, to3], "text", extraLength: -1)
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfNodeIsForeign()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("node", out var node);

        Mermaid
            .Unsafe
            .Flowchart()
            .AddCallback(node, "functionName")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfFunctionNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("node", out var node)
            .AddCallback(node, " ")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfTooltipIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("node", out var node)
            .AddCallback(node, "functionName", " ")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfNodeIsForeign()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("node", out var node);

        Mermaid
            .Unsafe
            .Flowchart()
            .AddHyperlink(node, "uri")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfUriIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("node", out var node)
            .AddHyperlink(node, " ")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfTooltipIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddNode("node", out var node)
            .AddHyperlink(node, "uri", " ")
            .Build();
    }

    [Fact]
    public void AddSubgraph_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .AddSubgraph(" ", out var _, builder => { })
            .Build();
    }

    [Fact]
    public void Comment_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .Flowchart()
            .Comment(" ")
            .Build();
    }
}
