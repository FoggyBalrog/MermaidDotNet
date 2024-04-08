namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

[Flags]
public enum EntityAttributeKeys
{
    None = 0,
    Primary = 1 << 0,
    Foreign = 1 << 1,
    Unique = 1 << 2
}
