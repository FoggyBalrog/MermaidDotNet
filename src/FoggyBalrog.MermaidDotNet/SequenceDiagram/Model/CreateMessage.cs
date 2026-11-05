namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

internal record CreateMessage(
    Member Sender,
    Member Recipient,
    string Description,
    LineType LineType,
    ArrowType ArrowType,
    ActivationType ActivationType) : Message(Sender, Recipient, Description, LineType, ArrowType, ActivationType);
