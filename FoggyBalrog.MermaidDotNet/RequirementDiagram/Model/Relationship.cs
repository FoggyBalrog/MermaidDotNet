namespace FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

internal record Relationship(IRequirementNode Source, IRequirementNode Target, RelationshipType Type);
