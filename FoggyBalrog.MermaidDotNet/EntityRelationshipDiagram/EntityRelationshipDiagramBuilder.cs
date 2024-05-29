using System.Text;
using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;

public class EntityRelationshipDiagramBuilder
{
    private readonly List<Entity> _entities = [];
    private readonly List<Relationship> _relationships = [];

    public EntityRelationshipDiagramBuilder AddEntity(string name, out Entity entity, params EntityAttribute[] attributes)
    {
        if (_entities.Exists(e => e.Name == name))
        {
            throw new InvalidOperationException($"Entity {name} already exists in the diagram");
        }

        entity = new Entity(name, attributes);
        _entities.Add(entity);
        return this;
    }

    public EntityRelationshipDiagramBuilder AddRelationship(
        Cardinality fromCardinality,
        Entity fromEntity,
        Cardinality toCardinality,
        Entity toEntity,
        string label,
        RelationshipType type = RelationshipType.Identifying)
    {
        if (!_entities.Contains(fromEntity))
        {
            throw new InvalidOperationException($"Entity {fromEntity.Name} does not exist in the diagram");
        }

        if (!_entities.Contains(toEntity))
        {
            throw new InvalidOperationException($"Entity {toEntity.Name} does not exist in the diagram");
        }

        _relationships.Add(new Relationship(fromCardinality, fromEntity, toCardinality, toEntity, label, type));
        return this;
    }

    public string Build()
    {
        const string indent = "    ";
        var builder = new StringBuilder();

        builder.AppendLine("erDiagram");

        foreach (Entity? entity in _entities.Where(e => e.Attributes.Length != 0 || !_relationships.Exists(r => r.FromEntity == e || r.ToEntity == e)))
        {
            builder.AppendLine($"{indent}{entity.Name} {{");

            foreach ((string type, string name, EntityAttributeKeys keys, string comment) in entity.Attributes)
            {
                IEnumerable<string> keyArray = Enum.GetValues(typeof(EntityAttributeKeys))
                    .Cast<EntityAttributeKeys>()
                    .Where(k => keys.HasFlag(k) && k != EntityAttributeKeys.None)
                    .Select(k => k switch
                    {
                        EntityAttributeKeys.Primary => "PK",
                        EntityAttributeKeys.Foreign => "FK",
                        EntityAttributeKeys.Unique => "UK",
                        _ => throw new InvalidOperationException($"Unknown key: {k}")
                    })
                    .ToList();

                string keyString = keyArray.Any() ? $" {string.Join(", ", keyArray)}" : string.Empty;

                string commentString = string.IsNullOrWhiteSpace(comment) ? string.Empty : $" \"{comment}\"";

                builder.AppendLine($"{indent}{indent}{type} {name}{keyString}{commentString}");
            }

            builder.AppendLine($"{indent}}}");
        }

        foreach (Relationship? relationship in _relationships)
        {
            string fromCardinalitySymbol = relationship.FromCardinality switch
            {
                Cardinality.ZeroOrOne => "|o",
                Cardinality.ExactlyOne => "||",
                Cardinality.ZeroOrMore => "}o",
                Cardinality.OneOrMore => "}|",
                _ => throw new InvalidOperationException($"Unknown cardinality: {relationship.FromCardinality}")
            };

            string toCardinalitySymbol = relationship.ToCardinality switch
            {
                Cardinality.ZeroOrOne => "o|",
                Cardinality.ExactlyOne => "||",
                Cardinality.ZeroOrMore => "o{",
                Cardinality.OneOrMore => "|{",
                _ => throw new InvalidOperationException($"Unknown cardinality: {relationship.ToCardinality}")
            };

            string line = relationship.Type switch
            {
                RelationshipType.Identifying => "--",
                RelationshipType.NonIdentifying => "..",
                _ => throw new InvalidOperationException($"Unknown relationship name: {relationship.Type}")
            };

            builder.AppendLine($"{indent}{relationship.FromEntity.Name} {fromCardinalitySymbol}{line}{toCardinalitySymbol} {relationship.ToEntity.Name} : \"{relationship.Label}\"");
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
