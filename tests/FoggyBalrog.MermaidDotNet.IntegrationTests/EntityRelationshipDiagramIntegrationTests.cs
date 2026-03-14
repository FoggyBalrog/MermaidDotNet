using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class EntityRelationshipDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithOnlyEntities()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out _, ("string", "foo"), ("int", "bar", EntityAttributeKeys.Primary | EntityAttributeKeys.Unique))
            .AddEntity("E2", out _, ("string", "baz", EntityAttributeKeys.Foreign, "hello"), ("int", "qux", "world"))
            .AddEntity("E3", out _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("Customer", out Entity c)
            .AddEntity("Order", out Entity o)
            .AddRelationship(Cardinality.ExactlyOne, c, Cardinality.ZeroOrMore, o, "places")
            .AddEntity("Product", out Entity p)
            .AddRelationship(Cardinality.ExactlyOne, o, Cardinality.OneOrMore, p, "contains")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithEachCardinality()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithIdentifyingRelationship()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out Entity e1)
            .AddEntity("E2", out Entity e2)
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "foo", RelationshipType.Identifying)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNonIdentifyingRelationship()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out Entity e1)
            .AddEntity("E2", out Entity e2)
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "foo", RelationshipType.NonIdentifying)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAttributes()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram()
            .AddEntity("E1", out Entity e1, ("string", "foo"), ("int", "bar", EntityAttributeKeys.Primary | EntityAttributeKeys.Unique))
            .AddEntity("E2", out Entity e2, ("string", "baz", EntityAttributeKeys.Foreign, "hello"), ("int", "qux", "world"))
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "has")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class EntityRelationshipDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithOnlyEntities()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("E1", out _, ("string", "foo"), ("int", "bar", EntityAttributeKeys.Primary | EntityAttributeKeys.Unique))
            .AddEntity("E2", out _, ("string", "baz", EntityAttributeKeys.Foreign, "hello"), ("int", "qux", "world"))
            .AddEntity("E3", out _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("Customer", out Entity c)
            .AddEntity("Order", out Entity o)
            .AddRelationship(Cardinality.ExactlyOne, c, Cardinality.ZeroOrMore, o, "places")
            .AddEntity("Product", out Entity p)
            .AddRelationship(Cardinality.ExactlyOne, o, Cardinality.OneOrMore, p, "contains")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithEachCardinality()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("E1", out Entity e1)
            .AddEntity("E2", out Entity e2)
            .AddEntity("E3", out Entity e3)
            .AddEntity("E4", out Entity e4)
            .AddRelationship(Cardinality.ZeroOrOne, e1, Cardinality.ZeroOrOne, e2, "foo")
            .AddRelationship(Cardinality.ExactlyOne, e2, Cardinality.ExactlyOne, e3, "foo")
            .AddRelationship(Cardinality.ZeroOrMore, e3, Cardinality.ZeroOrMore, e4, "foo")
            .AddRelationship(Cardinality.OneOrMore, e4, Cardinality.OneOrMore, e1, "foo")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithIdentifyingRelationship()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("E1", out Entity e1)
            .AddEntity("E2", out Entity e2)
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "foo", RelationshipType.Identifying)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNonIdentifyingRelationship()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("E1", out Entity e1)
            .AddEntity("E2", out Entity e2)
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "foo", RelationshipType.NonIdentifying)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAttributes()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("E1", out Entity e1, ("string", "foo"), ("int", "bar", EntityAttributeKeys.Primary | EntityAttributeKeys.Unique))
            .AddEntity("E2", out Entity e2, ("string", "baz", EntityAttributeKeys.Foreign, "hello"), ("int", "qux", "world"))
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ZeroOrMore, e2, "has")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("er", toolingFixture.GetDiagramType(diagramResult));
    }
}
