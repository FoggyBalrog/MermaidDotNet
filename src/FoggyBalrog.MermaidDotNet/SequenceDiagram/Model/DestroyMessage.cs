namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

internal record DestroyMessage(
    Member Sender,
    Member Recipient,
    string Description,
    LineType LineType,
    ArrowType ArrowType,
    DestructionTarget Target,
    ActivationType ActivationType) : Message(Sender, Recipient, Description, LineType, ArrowType, ActivationType);
