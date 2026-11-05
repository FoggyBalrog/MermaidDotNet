namespace FoggyBalrog.MermaidDotNet.MindMap.Model;

/// <summary>
/// Represents a node in a mind map.
/// </summary>
public record Node
{
    private readonly List<Node> _children = [];

    internal Node(string text, NodeShape shape, bool isMarkdown, string? icon, string[]? classes)
    {
        Text = text;
        Shape = shape;
        IsMarkdown = isMarkdown;
        Icon = icon;
        Classes = classes;
    }

    /// <summary>
    /// The text of the node.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// The shape of the node.
    /// </summary>
    public NodeShape Shape { get; }

    public bool IsMarkdown { get; }

    internal string? Icon { get; }

    internal string[]? Classes { get; }

    internal IReadOnlyCollection<Node> Children => _children.AsReadOnly();

    internal void AddChild(Node node)
    {
        _children.Add(node);
    }
}