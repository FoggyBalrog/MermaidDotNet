using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class EntityRelationshipDiagramSafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .Build();

        Assert.Equal("erDiagram", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithOnlyEntities()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out _, ("string", "foo"), ("int", "bar", EntityAttributeKeys.Primary | EntityAttributeKeys.Unique))
            .AddEntity("E2", out _, ("string", "baz", EntityAttributeKeys.Foreign, "hello"), ("int", "qux", "world"))
            .AddEntity("E3", out _)
            .Build();

        Assert.Equal(@"erDiagram
    E1 {
        string foo
        int bar PK, UK
    }
    E2 {
        string baz FK ""hello""
        int qux ""world""
    }
    E3 {
    }", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("Customer", out Entity c)
            .AddEntity("Order", out Entity o)
            .AddRelationship(Cardinality.ExactlyOne, c, Cardinality.ZeroOrMore, o, "places")
            .AddEntity("Product", out Entity p)
            .AddRelationship(Cardinality.ExactlyOne, o, Cardinality.OneOrMore, p, "contains")
            .Build();

        Assert.Equal(@"erDiagram
    Customer ||--o{ Order : ""places""
    Order ||--|{ Product : ""contains""", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithEachCardinality()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out Entity e1)
            .AddEntity("E2", out Entity e2)
            .AddEntity("E3", out Entity e3)
            .AddEntity("E4", out Entity e4)
            .AddRelationship(Cardinality.ZeroOrOne, e1, Cardinality.ZeroOrOne, e2, "foo")
            .AddRelationship(Cardinality.ExactlyOne, e2, Cardinality.ExactlyOne, e3, "foo")
            .AddRelationship(Cardinality.ZeroOrMore, e3, Cardinality.ZeroOrMore, e4, "foo")
            .AddRelationship(Cardinality.OneOrMore, e4, Cardinality.OneOrMore, e1, "foo")
            .Build();

        Assert.Equal(@"erDiagram
    E1 |o--o| E2 : ""foo""
    E2 ||--|| E3 : ""foo""
    E3 }o--o{ E4 : ""foo""
    E4 }|--|{ E1 : ""foo""", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithIdentifyingRelationship()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out Entity e1)
            .AddEntity("E2", out Entity e2)
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "foo", RelationshipType.Identifying)
            .Build();

        Assert.Equal(@"erDiagram
    E1 ||--o{ E2 : ""foo""", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithNonIdentifyingRelationship()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out Entity e1)
            .AddEntity("E2", out Entity e2)
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "foo", RelationshipType.NonIdentifying)
            .Build();

        Assert.Equal(@"erDiagram
    E1 ||..o{ E2 : ""foo""", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithAttributes()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out Entity e1, ("string", "foo"), ("int", "bar", EntityAttributeKeys.Primary | EntityAttributeKeys.Unique))
            .AddEntity("E2", out Entity e2, ("string", "baz", EntityAttributeKeys.Foreign, "hello"), ("int", "qux", "world"))
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "has")
            .Build();

        Assert.Equal(@"erDiagram
    E1 {
        string foo
        int bar PK, UK
    }
    E2 {
        string baz FK ""hello""
        int qux ""world""
    }
    E1 ||--o{ E2 : ""has""", diagram, ignoreLineEndingDifferences: true);
    }
}
