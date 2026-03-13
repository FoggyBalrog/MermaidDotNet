using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.Flowchart;

public class FlowchartNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void AddNode_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddNodeWithExpandedShape_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNodeWithExpandedShape(" ", out var _, ExpandedNodeShape.NotchRect)
            .Build();
    }

    [Fact]
    public void AddMarkdownNode_DoesNotThrowIfMarkdownIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddMarkdownNode(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfFromIsForeign()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("from", out var from);

        Mermaid
            .Flowchart(options: _options)
            .AddNode("to", out var to)
            .AddLink(from, to, out _, "text")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfToIsForeign()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("to", out var to);

        Mermaid
            .Flowchart(options: _options)
            .AddNode("from", out var from)
            .AddLink(from, to, out _, "text")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("from", out var from)
            .AddNode("to", out var to)
            .AddLink(from, to, out _, " ")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfExtraLengthIsStrictlyNegative()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("from", out var from)
            .AddNode("to", out var to)
            .AddLink(from, to, out _, "text", extraLength: -1)
            .Build();
    }

    [Fact]
    public void AddLinkChain_DoesNotThrowIfFromIsForeign()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("from2", out var from2);

        Mermaid
            .Flowchart(options: _options)
            .AddNode("from1", out var from1)
            .AddNode("from3", out var from3)
            .AddNode("to1", out var to1)
            .AddNode("to2", out var to2)
            .AddNode("to3", out var to3)
            .AddLinkChain([from1, from2, from3], [to1, to2, to3], out _, "text")
            .Build();
    }

    [Fact]
    public void AddLinkChain_DoesNotThrowIfToIsForeign()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("to2", out var to2);

        Mermaid
            .Flowchart(options: _options)
            .AddNode("from1", out var from1)
            .AddNode("from2", out var from2)
            .AddNode("from3", out var from3)
            .AddNode("to1", out var to1)
            .AddNode("to3", out var to3)
            .AddLinkChain([from1, from2, from3], [to1, to2, to3], out _, "text")
            .Build();
    }

    [Fact]
    public void AddLinkChain_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("from1", out var from1)
            .AddNode("from2", out var from2)
            .AddNode("from3", out var from3)
            .AddNode("to1", out var to1)
            .AddNode("to2", out var to2)
            .AddNode("to3", out var to3)
            .AddLinkChain([from1, from2, from3], [to1, to2, to3], out _, " ")
            .Build();
    }

    [Fact]
    public void AddLinkChain_DoesNotThrowIfExtraLengthIsStrictlyNegative()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("from1", out var from1)
            .AddNode("from2", out var from2)
            .AddNode("from3", out var from3)
            .AddNode("to1", out var to1)
            .AddNode("to2", out var to2)
            .AddNode("to3", out var to3)
            .AddLinkChain([from1, from2, from3], [to1, to2, to3], out _, "text", extraLength: -1)
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfNodeIsForeign()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("node", out var node);

        Mermaid
            .Flowchart(options: _options)
            .AddCallback(node, "functionName")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfFunctionNameIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("node", out var node)
            .AddCallback(node, " ")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfTooltipIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("node", out var node)
            .AddCallback(node, "functionName", " ")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfNodeIsForeign()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("node", out var node);

        Mermaid
            .Flowchart(options: _options)
            .AddHyperlink(node, "uri")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfUriIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("node", out var node)
            .AddHyperlink(node, " ")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfTooltipIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("node", out var node)
            .AddHyperlink(node, "uri", " ")
            .Build();
    }

    [Fact]
    public void AddSubgraph_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddSubgraph(" ", out var _, builder => { })
            .Build();
    }

    [Fact]
    public void Comment_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .Comment(" ")
            .Build();
    }

    [Fact]
    public void StyleLinks_DoesNotThrowIfCssIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddLink(n1, n2, out Link l1)
            .AddLink(n2, n1, out Link l2)
            .AddLink(n1, n2, out Link l3)
            .StyleLinks(" ", l1, l2, l3)
            .Build();
    }

    [Fact]
    public void StyleLinks_DoesNotThrowIfNoLinksAreProvided()
    {
        Mermaid
            .Flowchart(options: _options)
            .StyleLinks(" ")
            .Build();
    }

    [Fact]
    public void StyleLinks_DoesNotThrowIfForeignLinkIsProvided()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddLink(n1, n2, out Link l1);

        Mermaid
            .Flowchart(options: _options)
            .AddNode("N3", out Node n3)
            .AddNode("N4", out Node n4)
            .AddLink(n3, n4, out Link l2)
            .StyleLinks(" ", l1, l2)
            .Build();
    }

    [Fact]
    public void StyleNodes_DoesNotThrowIfCssIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .StyleNodes(" ", n1, n2, n3)
            .Build();
    }

    [Fact]
    public void StyleNodes_DoesNotThrowIfCssClassIsForeign()
    {
        Mermaid
            .Flowchart(options: _options)
            .DefineCssClass("foo", "bar", out CssClass cssClass);

        Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N2", out Node n2)
            .AddNode("N3", out Node n3)
            .StyleNodes(cssClass, n1, n2, n3)
            .Build();
    }

    [Fact]
    public void StyleNodes_DoesNotThrowIfNoNodesAreProvided()
    {
        Mermaid
            .Flowchart(options: _options)
            .DefineCssClass("foo", "bar", out CssClass cssClass)
            .StyleNodes(" ")
            .StyleNodes(cssClass)
            .Build();
    }

    [Fact]
    public void StyleNodes_DoesNotThrowIfForeignNodeIsProvided()
    {
        Mermaid
            .Flowchart(options: _options)
            .AddNode("N2", out Node n2);

        Mermaid
            .Flowchart(options: _options)
            .AddNode("N1", out Node n1)
            .AddNode("N3", out Node n3)
            .DefineCssClass("foo", "bar", out CssClass cssClass)
            .StyleNodes(" ", n1, n2, n3)
            .StyleNodes(cssClass, n1, n2, n3)
            .Build();
    }

    [Fact]
    public void DefineCssClass_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .DefineCssClass(" ", "css", out var _)
            .Build();
    }

    [Fact]
    public void DefineCssClass_DoesNotThrowIfCssIsWhiteSpace()
    {
        Mermaid
            .Flowchart(options: _options)
            .DefineCssClass("foo", " ", out var _)
            .Build();
    }
}
