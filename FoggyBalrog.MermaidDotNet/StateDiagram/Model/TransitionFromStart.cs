namespace FoggyBalrog.MermaidDotNet.StateDiagram.Model;

internal record TransitionFromStart(State To, string? Description) : IStateDiagramItem;
