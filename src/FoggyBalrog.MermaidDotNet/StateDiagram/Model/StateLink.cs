namespace FoggyBalrog.MermaidDotNet.StateDiagram.Model;

/// <summary>
/// Represents a navigational link associated with a state in a state diagram.
/// </summary>
internal record StateLink : IStateDiagramItem
{
    internal StateLink(State state, string url, string? tooltip)
    {
        State = state;
        Url = url;
        Tooltip = tooltip;
    }

    /// <summary>
    /// The state associated with the link.
    /// </summary>
    public State State { get; }

    /// <summary>
    /// The URL of the link.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// An optional tooltip for the link.
    /// </summary>
    public string? Tooltip { get; }
}