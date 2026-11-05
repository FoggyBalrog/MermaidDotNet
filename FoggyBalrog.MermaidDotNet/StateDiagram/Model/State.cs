namespace FoggyBalrog.MermaidDotNet.StateDiagram.Model;

/// <summary>
/// Represents a state in a state diagram.
/// </summary>
public record State : IStateDiagramItem
{
    internal State(string id, string description, StateKind kind, StateDiagramDirection? direction = null)
    {
        Id = id;
        Description = description;
        Kind = kind;
        Direction = direction;
    }

    /// <summary>
    /// The unique identifier of the state.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The description of the state.
    /// </summary>
    public string Description { get; }

    internal StateKind Kind { get; }

    internal StateDiagramDirection? Direction { get; }
}
