namespace FoggyBalrog.MermaidDotNet.StateDiagram.Model;

internal record Note(State State, NotePosition Position, string Text) : IStateDiagramItem;
