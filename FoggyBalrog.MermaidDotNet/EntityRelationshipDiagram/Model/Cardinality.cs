namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

/// <summary>
/// The cardinality of a relationship
/// </summary>
public enum Cardinality
{
    ZeroOrOne,
    ExactlyOne,
    ZeroOrMore,
    OneOrMore
}