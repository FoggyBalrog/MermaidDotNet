namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

internal record Link(Member Member, string Title, string Uri) : ISequenceItem;
