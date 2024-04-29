namespace FoggyBalrog.MermaidDotNet.StateDiagram.Model;

public record State : IStateDiagramItem
{
    internal State(string id, string description, StateKind kind)
    {
        Id = id;
        Description = description;
        Kind = kind;
    }

    public string Id { get; }
    public string Description { get; }
    internal StateKind Kind { get; }
}
