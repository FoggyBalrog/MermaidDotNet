﻿using System.Text;
using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;

/// <summary>
/// A builder for creating entity relationship diagrams.
/// </summary>
public class EntityRelationshipDiagramBuilder
{
    private readonly List<Entity> _entities = [];
    private readonly List<Relationship> _relationships = [];

    internal EntityRelationshipDiagramBuilder()
    {
    }

    /// <summary>
    /// Adds an entity to the diagram.
    /// </summary>
    /// <param name="name">The name of the entity.</param>
    /// <param name="entity">The entity that was added.</param>
    /// <param name="attributes">Optional attributes for the entity.</param>
    /// <returns>The current <see cref="EntityRelationshipDiagramBuilder"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an entity with the same name already exists in the diagram.</exception>
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

    /// <summary>
    /// Adds a relationship between two entities in the diagram.
    /// </summary>
    /// <param name="fromCardinality">The cardinality of the relationship relative to <paramref name="fromEntity"/>.</param>
    /// <param name="fromEntity">The entity that the relationship starts from.</param>
    /// <param name="toCardinality">The cardinality of the relationship relative to <paramref name="toEntity"/>.</param>
    /// <param name="toEntity">The entity that the relationship goes to.</param>
    /// <param name="label">The label of the relationship.</param>
    /// <param name="type">The type of the relationship.</param>
    /// <returns>The current <see cref="EntityRelationshipDiagramBuilder"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when either <paramref name="fromEntity"/> or <paramref name="toEntity"/> does not exist in the diagram.</exception>
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

    /// <summary>
    /// Builds the Mermaid code for the entity relationship diagram.
    /// </summary>
    /// <returns>The Mermaid code for the entity relationship diagram.</returns>
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
