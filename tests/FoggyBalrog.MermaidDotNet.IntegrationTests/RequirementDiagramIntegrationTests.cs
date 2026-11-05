using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class RequirementDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildSimpleRequirementDiagram()
    {
        string diagram = Mermaid
            .RequirementDiagram()
            .AddRequirement("Requirement 1", out var requirement1)
            .AddRequirement("Requirement 2", out var requirement2)
            .AddElement("Element 1", out var element1)
            .AddElement("Element 2", out var element2)
            .AddRelationship(element1, requirement1, RelationshipType.Satisfies)
            .AddRelationship(element2, requirement2, RelationshipType.Satisfies)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("requirement", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildRequirementDiagramWithRequirementDetails()
    {
        string diagram = Mermaid
            .RequirementDiagram()
            .AddRequirement("Requirement 1", out var requirement1, "REQ-001", "This is a requirement", RequirementType.Interface, RequirementRisk.High, RequirementVerificationMethod.Inspection)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("requirement", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildRequirementDiagramWithElementDetails()
    {
        string diagram = Mermaid
            .RequirementDiagram()
            .AddElement("Element 1", out var element1, "Type 1", "https://example.com/doc1")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("requirement", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildRequirementDiagramWithRelationshipsTypes()
    {
        string diagram = Mermaid
            .RequirementDiagram()
            .AddRequirement("Requirement 1", out var requirement1)
            .AddElement("Element 1", out var element1)
            .AddElement("Element 2", out var element2)
            .AddElement("Element 3", out var element3)
            .AddElement("Element 4", out var element4)
            .AddElement("Element 5", out var element5)
            .AddElement("Element 6", out var element6)
            .AddElement("Element 7", out var element7)
            .AddRelationship(element1, requirement1, RelationshipType.Contains)
            .AddRelationship(element2, requirement1, RelationshipType.Copies)
            .AddRelationship(element3, requirement1, RelationshipType.Derives)
            .AddRelationship(element4, requirement1, RelationshipType.Satisfies)
            .AddRelationship(element5, requirement1, RelationshipType.Verifies)
            .AddRelationship(element6, requirement1, RelationshipType.Refines)
            .AddRelationship(element7, requirement1, RelationshipType.Traces)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("requirement", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class RequirementDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildSimpleRequirementDiagram()
    {
        string diagram = Mermaid
            .RequirementDiagram(options: _options)
            .AddRequirement("Requirement 1", out var requirement1)
            .AddRequirement("Requirement 2", out var requirement2)
            .AddElement("Element 1", out var element1)
            .AddElement("Element 2", out var element2)
            .AddRelationship(element1, requirement1, RelationshipType.Satisfies)
            .AddRelationship(element2, requirement2, RelationshipType.Satisfies)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("requirement", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildRequirementDiagramWithRequirementDetails()
    {
        string diagram = Mermaid
            .RequirementDiagram(options: _options)
            .AddRequirement("Requirement 1", out var requirement1, "REQ-001", "This is a requirement", RequirementType.Interface, RequirementRisk.High, RequirementVerificationMethod.Inspection)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("requirement", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildRequirementDiagramWithElementDetails()
    {
        string diagram = Mermaid
            .RequirementDiagram(options: _options)
            .AddElement("Element 1", out var element1, "Type 1", "https://example.com/doc1")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("requirement", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildRequirementDiagramWithRelationshipsTypes()
    {
        string diagram = Mermaid
            .RequirementDiagram(options: _options)
            .AddRequirement("Requirement 1", out var requirement1)
            .AddElement("Element 1", out var element1)
            .AddElement("Element 2", out var element2)
            .AddElement("Element 3", out var element3)
            .AddElement("Element 4", out var element4)
            .AddElement("Element 5", out var element5)
            .AddElement("Element 6", out var element6)
            .AddElement("Element 7", out var element7)
            .AddRelationship(element1, requirement1, RelationshipType.Contains)
            .AddRelationship(element2, requirement1, RelationshipType.Copies)
            .AddRelationship(element3, requirement1, RelationshipType.Derives)
            .AddRelationship(element4, requirement1, RelationshipType.Satisfies)
            .AddRelationship(element5, requirement1, RelationshipType.Verifies)
            .AddRelationship(element6, requirement1, RelationshipType.Refines)
            .AddRelationship(element7, requirement1, RelationshipType.Traces)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("requirement", toolingFixture.GetDiagramType(diagramResult));
    }
}
