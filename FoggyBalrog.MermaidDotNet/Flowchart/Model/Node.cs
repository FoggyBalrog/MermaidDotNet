namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

/// <summary>
/// Represents a node in a flowchart.
/// </summary>
public record Node : ILinkable
{
    internal Node(string id, string text, NodeShape? shape, INodeClickBinding? nodeClickBinding, ExpandedNodeShape? expandedShape = null)
    {
        Id = id;
        Text = text;
        Shape = shape;
        NodeClickBinding = nodeClickBinding;
        ExpandedShape = expandedShape;
    }

    /// <summary>
    /// The unique identifier of the node.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The text of the node.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// The shape of the node.
    /// </summary>
    public NodeShape? Shape { get; }

    /// <summary>
    /// The expanded shape of the node.
    /// </summary>
    public ExpandedNodeShape? ExpandedShape { get; }

    /// <summary>
    /// An optional binding for a click event on the node, that can be either a URL or a JavaScript function.
    /// </summary>
    internal INodeClickBinding? NodeClickBinding { get; set; }
}
