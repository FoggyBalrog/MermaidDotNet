namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

/// <summary>
/// The keys for the attributes of an entity. Can be combined, e.g. <c> EntityAttributeKeys.Primary | EntityAttributeKeys.Unique </c>
/// </summary>
[Flags]
public enum EntityAttributeKeys
{
    None = 0,
    Primary = 1 << 0,
    Foreign = 1 << 1,
    Unique = 1 << 2
}
