namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

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
