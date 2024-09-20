using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.EntityRelationshipDiagram;

public class EntityRelationshipDiagramSafeModeValidationTests
{
    [Fact]
    public void AddEntity_ThrowsIfWhitespaceName()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .EntityRelationshipDiagram()
                .AddEntity(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddEntity_ThrowsIfNameIsDuplicate()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .EntityRelationshipDiagram()
                .AddEntity("Entity", out var _)
                .AddEntity("Entity", out var _);
        });

        Assert.Equal(MermaidExceptionReason.DuplicateValue, exception.Reason);
    }

    [Fact]
    public void AddRelationship_ThrowsIfFromEntityIsNotPartOfDiagram()
    {
        Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("foo", out var e1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .EntityRelationshipDiagram()
                .AddEntity("bar", out var e2)
                .AddRelationship(Cardinality.OneOrMore, e1, Cardinality.ExactlyOne, e2, "label");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddRelationship_ThrowsIfToEntityIsNotPartOfDiagram()
    {
        Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("foo", out var e1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .EntityRelationshipDiagram()
                .AddEntity("bar", out var e2)
                .AddRelationship(Cardinality.OneOrMore, e2, Cardinality.ExactlyOne, e1, "label");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddRelationship_ThrowsIfLabelIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .EntityRelationshipDiagram()
                .AddEntity("foo", out var e1)
                .AddEntity("bar", out var e2)
                .AddRelationship(Cardinality.OneOrMore, e1, Cardinality.ExactlyOne, e2, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}
