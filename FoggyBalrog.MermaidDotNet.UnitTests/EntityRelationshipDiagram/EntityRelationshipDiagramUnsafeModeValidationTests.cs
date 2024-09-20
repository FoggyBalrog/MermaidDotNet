using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.EntityRelationshipDiagram;

public class EntityRelationshipDiagramUnsafeModeValidationTests
{
    [Fact]
    public void AddEntity_DoesNotThrowIfWhitespaceName()
    {
        Mermaid
            .Unsafe
            .EntityRelationshipDiagram()
            .AddEntity(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddEntity_DoesNotThrowIfNameIsDuplicate()
    {
        Mermaid
            .Unsafe
            .EntityRelationshipDiagram()
            .AddEntity("Entity", out var _)
            .AddEntity("Entity", out var _)
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfFromEntityIsNotPartOfDiagram()
    {
        Mermaid
            .Unsafe
            .EntityRelationshipDiagram()
            .AddEntity("foo", out var e1);

        Mermaid
            .Unsafe
            .EntityRelationshipDiagram()
            .AddEntity("bar", out var e2)
            .AddRelationship(Cardinality.OneOrMore, e1, Cardinality.ExactlyOne, e2, "label")
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfToEntityIsNotPartOfDiagram()
    {
        Mermaid
            .Unsafe
            .EntityRelationshipDiagram()
            .AddEntity("foo", out var e1);

        Mermaid
            .Unsafe
            .EntityRelationshipDiagram()
            .AddEntity("bar", out var e2)
            .AddRelationship(Cardinality.OneOrMore, e2, Cardinality.ExactlyOne, e1, "label")
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfLabelIsWhitespace()
    {
        Mermaid
            .Unsafe
            .EntityRelationshipDiagram()
            .AddEntity("foo", out var e1)
            .AddEntity("bar", out var e2)
            .AddRelationship(Cardinality.OneOrMore, e1, Cardinality.ExactlyOne, e2, " ")
            .Build();
    }
}
