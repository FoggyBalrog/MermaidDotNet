namespace FoggyBalrog.MermaidDotNet.UnitTests.MindMap;

public class MindMapSafeModeValidationTests
{
    [Fact]
    public void MindMapBuilder_ThrowsIfRootTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.MindMap(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void MindMapBuilder_ThrowsIfMarkdownWithDefaultShape()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.MindMap("Root", rootIsMarkdown: true);
        });

        Assert.Equal(MermaidExceptionReason.InvalidOperation, exception.Reason);
    }

    [Fact]
    public void AddNode_ThrowsIfNodeTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .MindMap("Root")
                .AddNode(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNode_ThrowsIfParentIsForeign()
    {
        Mermaid
            .MindMap("Root")
            .AddNode("Node 1", out var node1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .MindMap("Root")
                .AddNode("Node 2", out var node2, node1);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddNode_ThrowsIfIconIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .MindMap("Root")
                .AddNode("Node 1", out var _, icon: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNode_ThrowsIfAnyClassIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .MindMap("Root")
                .AddNode("Node 1", out var _, classes: ["foo", " ", "baz"]);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNode_ThrowsIfMarkdownWithDefaultShape()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .MindMap("Root")
                .AddNode("Node 1", out var _, isMarkdown: true);
        });

        Assert.Equal(MermaidExceptionReason.InvalidOperation, exception.Reason);
    }
}
