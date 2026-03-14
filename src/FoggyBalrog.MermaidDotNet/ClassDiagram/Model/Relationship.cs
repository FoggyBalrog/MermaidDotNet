namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

internal record Relationship(
    Class From,
    Class To,
    RelationshipType FromRelationshipType,
    Cardinality? FromCardinality,
    RelationshipType ToRelationshipType,
    Cardinality? ToCardinality,
    LinkStyle LinkStyle,
    string? Label);
