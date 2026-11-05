using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.EntityRelationshipDiagram;

public class EntityRelationshipDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void AddEntity_DoesNotThrowIfWhitespaceName()
    {
        Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddEntity_DoesNotThrowIfNameIsDuplicate()
    {
        Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("Entity", out var _)
            .AddEntity("Entity", out var _)
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfFromEntityIsNotPartOfDiagram()
    {
        Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("foo", out var e1);

        Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("bar", out var e2)
            .AddRelationship(Cardinality.OneOrMore, e1, Cardinality.ExactlyOne, e2, "label")
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfToEntityIsNotPartOfDiagram()
    {
        Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("foo", out var e1);

        Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("bar", out var e2)
            .AddRelationship(Cardinality.OneOrMore, e2, Cardinality.ExactlyOne, e1, "label")
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfLabelIsWhitespace()
    {
        Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("foo", out var e1)
            .AddEntity("bar", out var e2)
            .AddRelationship(Cardinality.OneOrMore, e1, Cardinality.ExactlyOne, e2, " ")
            .Build();
    }
}
