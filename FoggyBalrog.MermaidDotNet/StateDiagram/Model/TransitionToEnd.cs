namespace FoggyBalrog.MermaidDotNet.StateDiagram.Model;

internal record TransitionToEnd(State From, string? Description) : IStateDiagramItem;
