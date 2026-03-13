namespace FoggyBalrog.MermaidDotNet.UnitTests.MindMap;

public class MindMapNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void MindMapBuilder_DoesNotThrowIfRootTextIsWhiteSpace()
    {
        Mermaid
            .MindMap(" ", options: _options)
            .Build();
    }

    [Fact]
    public void MindMapBuilder_DoesNotThrowIfMarkdownWithDefaultShape()
    {
        Mermaid
            .MindMap("Root", rootIsMarkdown: true, options: _options)
            .Build();
    }

    [Fact]
    public void AddNode_DoesNotThrowIfNodeTextIsWhiteSpace()
    {
        Mermaid
            .MindMap("Root", options: _options)
            .AddNode(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddNode_DoesNotThrowIfParentIsForeign()
    {
        Mermaid
            .MindMap("Root", options: _options)
            .AddNode("Node 1", out var node1);

        Mermaid
            .MindMap("Root", options: _options)
            .AddNode("Node 2", out var node2, node1)
            .Build();
    }

    [Fact]
    public void AddNode_DoesNotThrowIfIconIsWhiteSpace()
    {
        Mermaid
            .MindMap("Root", options: _options)
            .AddNode("Node 1", out var _, icon: " ")
            .Build();
    }

    [Fact]
    public void AddNode_DoesNotThrowIfAnyClassIsWhiteSpace()
    {
        Mermaid
            .MindMap("Root", options: _options)
            .AddNode("Node 1", out var _, classes: ["foo", " ", "baz"])
            .Build();
    }

    [Fact]
    public void AddNode_DoesNotThrowIfMarkdownWithDefaultShape()
    {
        Mermaid
            .MindMap("Root", rootIsMarkdown: true, options: _options)
            .AddNode("Node 1", out var _, isMarkdown: true)
            .Build();
    }
}
