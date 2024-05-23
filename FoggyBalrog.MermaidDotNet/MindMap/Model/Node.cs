namespace FoggyBalrog.MermaidDotNet.MindMap.Model;

public record Node
{
    private readonly List<Node> _children = [];

    internal Node(string text, NodeShape shape)
    {
        Text = text;
        Shape = shape;
    }

    public string Text { get; }
    public NodeShape Shape { get; }

    public IReadOnlyCollection<Node> Children => _children.AsReadOnly();

    internal void AddChild(Node node)
    {
        _children.Add(node);
    }
}