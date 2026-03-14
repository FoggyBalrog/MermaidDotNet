namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

internal record Message(
    Member Sender,
    Member Recipient,
    string Description,
    LineType LineType,
    ArrowType ArrowType,
    ActivationType ActivationType) : ISequenceItem;
