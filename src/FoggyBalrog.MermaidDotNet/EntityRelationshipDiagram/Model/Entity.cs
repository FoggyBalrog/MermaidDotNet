namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

/// <summary>
/// Represents an entity in an entity relationship diagram.
/// </summary>
public record Entity
{
    internal Entity(string name, EntityAttribute[] attributes)
    {
        Name = name;
        Attributes = attributes;
    }

    public string Name { get; }
    public EntityAttribute[] Attributes { get; }
}
