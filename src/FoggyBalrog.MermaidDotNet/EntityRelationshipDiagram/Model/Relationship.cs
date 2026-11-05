namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

internal record Relationship(
    Cardinality FromCardinality,
    Entity FromEntity,
    Cardinality ToCardinality,
    Entity ToEntity,
    string Label,
    RelationshipType Type);
