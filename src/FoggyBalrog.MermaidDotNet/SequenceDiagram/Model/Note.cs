namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

internal record Note(string Text, NotePosition Position, Member[] Members) : ISequenceItem;
