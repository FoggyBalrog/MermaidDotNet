namespace FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

/// <summary>
/// The relationship type between two nodes.
/// </summary>
public enum RelationshipType
{
    Contains,
    Copies,
    Derives,
    Satisfies,
    Verifies,
    Refines,
    Traces
}