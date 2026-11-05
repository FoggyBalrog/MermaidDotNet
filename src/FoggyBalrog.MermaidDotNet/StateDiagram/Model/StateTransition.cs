namespace FoggyBalrog.MermaidDotNet.StateDiagram.Model;

internal record StateTransition(State From, State To, string? Description) : IStateDiagramItem;
