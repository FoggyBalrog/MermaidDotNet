namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

public record Node : ILinkable
{
    internal Node(string id, string text, NodeShape shape, INodeClickBinding? nodeClickBinding)
    {
        Id = id;
        Text = text;
        Shape = shape;
        NodeClickBinding = nodeClickBinding;
    }

    public string Id { get; }
    public string Text { get; }
    public NodeShape Shape { get; }
    internal INodeClickBinding? NodeClickBinding { get; set; }
}
