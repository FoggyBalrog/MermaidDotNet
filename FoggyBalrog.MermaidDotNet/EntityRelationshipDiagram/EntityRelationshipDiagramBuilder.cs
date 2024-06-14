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
        var builder = new StringBuilder();

        builder.AppendLine("erDiagram");

        foreach (Entity? entity in _entities.Where(e => e.Attributes.Length != 0 || !_relationships.Exists(r => r.FromEntity == e || r.ToEntity == e)))
        {
            builder.AppendLine($"{Shared.Indent}{entity.Name} {{");

            foreach ((string type, string name, EntityAttributeKeys keys, string comment) in entity.Attributes)
            {
                IEnumerable<string> keyArray = Enum.GetValues(typeof(EntityAttributeKeys))
                    .Cast<EntityAttributeKeys>()
                    .Where(k => keys.HasFlag(k) && k != EntityAttributeKeys.None)
                    .Select(k => SymbolMaps.Keys[k])
                    .ToList();

                string keyString = keyArray.Any() ? $" {string.Join(", ", keyArray)}" : string.Empty;

                string commentString = string.IsNullOrWhiteSpace(comment) ? string.Empty : $" \"{comment}\"";

                builder.AppendLine($"{Shared.Indent.Repeat(2)}{type} {name}{keyString}{commentString}");
            }

            builder.AppendLine($"{Shared.Indent}}}");
        }

        foreach (Relationship? relationship in _relationships)
        {
            string fromCardinalitySymbol = SymbolMaps.Cardinalities[relationship.FromCardinality].From;
            string toCardinalitySymbol = SymbolMaps.Cardinalities[relationship.ToCardinality].To;
            string line = SymbolMaps.Relationships[relationship.Type];

            builder.AppendLine($"{Shared.Indent}{relationship.FromEntity.Name} {fromCardinalitySymbol}{line}{toCardinalitySymbol} {relationship.ToEntity.Name} : \"{relationship.Label}\"");
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
