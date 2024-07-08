namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

/// <summary>
/// Represents a subgraph in a flowchart.
/// </summary>
public record Subgraph : ILinkable
{
    internal Subgraph(string id, string text, FlowchartOrientation? direction)
    {
        Id = id;
        Text = text;
        Direction = direction;
    }

    /// <summary>
    /// The unique identifier of the subgraph.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The text of the subgraph.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// An optional direction of the subgraph.
    /// </summary>
    public FlowchartOrientation? Direction { get; }
}
