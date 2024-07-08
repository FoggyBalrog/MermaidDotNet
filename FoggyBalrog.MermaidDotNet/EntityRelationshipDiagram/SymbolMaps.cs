using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;

internal static class SymbolMaps
{
    public static Dictionary<EntityAttributeKeys, string> Keys { get; } = new()
    {
        [EntityAttributeKeys.Primary] = "PK",
        [EntityAttributeKeys.Foreign] = "FK",
        [EntityAttributeKeys.Unique] = "UK",
    };

    public static Dictionary<Cardinality, (string From, string To)> Cardinalities { get; } = new()
    {
        [Cardinality.ZeroOrOne] = ("|o", "o|"),
        [Cardinality.ExactlyOne] = ("||", "||"),
        [Cardinality.ZeroOrMore] = ("}o", "o{"),
        [Cardinality.OneOrMore] = ("}|", "|{")
    };

    public static Dictionary<RelationshipType, string> Relationships { get; } = new()
    {
        [RelationshipType.Identifying] = "--",
        [RelationshipType.NonIdentifying] = ".."
    };
}
