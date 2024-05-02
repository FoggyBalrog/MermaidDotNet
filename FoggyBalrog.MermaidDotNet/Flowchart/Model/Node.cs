namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

public record Node : ILinkable
{
    internal Node(string id, string text, NodeShape shape, INodeClickBindind? nodeClickBindind)
    {
        Id = id;
        Text = text;
        Shape = shape;
        NodeClickBindind = nodeClickBindind;
    }

    public string Id { get; }
    public string Text { get; }
    public NodeShape Shape { get; }
    internal INodeClickBindind? NodeClickBindind { get; set; }
}
