namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

public record Subgraph : ILinkable
{
    internal Subgraph(string id, string text, FlowchartOrientation? direction)
    {
        Id = id;
        Text = text;
        Direction = direction;
    }

    public string Id { get; }
    public string Text { get; }
    public FlowchartOrientation? Direction { get; }
}
