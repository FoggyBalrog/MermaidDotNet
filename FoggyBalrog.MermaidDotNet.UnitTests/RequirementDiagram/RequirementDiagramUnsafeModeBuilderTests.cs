using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.RequirementDiagram;

public class RequirementDiagramUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildSimpleRequirementDiagram()
    {
        string diagram = Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddRequirement("Requirement 1", out var requirement1)
            .AddRequirement("Requirement 2", out var requirement2)
            .AddElement("Element 1", out var element1)
            .AddElement("Element 2", out var element2)
            .AddRelationship(element1, requirement1, RelationshipType.Satisfies)
            .AddRelationship(element2, requirement2, RelationshipType.Satisfies)
            .Build();

        Assert.Equal(@"requirementDiagram
    requirement ""Requirement 1"" {
    }
    requirement ""Requirement 2"" {
    }
    element ""Element 1"" {
    }
    element ""Element 2"" {
    }
    ""Element 1"" - satisfies -> ""Requirement 1""
    ""Element 2"" - satisfies -> ""Requirement 2""", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildRequirementDiagramWithRequirementDetails()
    {
        string diagram = Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddRequirement("Requirement 1", out var requirement1, "REQ-001", "This is a requirement", RequirementType.Interface, RequirementRisk.High, RequirementVerificationMethod.Inspection)
            .Build();

        Assert.Equal(@"requirementDiagram
    interfaceRequirement ""Requirement 1"" {
        id: ""REQ-001""
        text: ""This is a requirement""
        risk: High
        verifyMethod: Inspection
    }", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildRequirementDiagramWithElementDetails()
    {
        string diagram = Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddElement("Element 1", out var element1, "Type 1", "https://example.com/doc1")
            .Build();

        Assert.Equal(@"requirementDiagram
    element ""Element 1"" {
        type: ""Type 1""
        docRef: ""https://example.com/doc1""
    }", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildRequirementDiagramWithRelationshipsTypes()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"requirementDiagram
    requirement ""Requirement 1"" {
    }
    element ""Element 1"" {
    }
    element ""Element 2"" {
    }
    element ""Element 3"" {
    }
    element ""Element 4"" {
    }
    element ""Element 5"" {
    }
    element ""Element 6"" {
    }
    element ""Element 7"" {
    }
    ""Element 1"" - contains -> ""Requirement 1""
    ""Element 2"" - copies -> ""Requirement 1""
    ""Element 3"" - derives -> ""Requirement 1""
    ""Element 4"" - satisfies -> ""Requirement 1""
    ""Element 5"" - verifies -> ""Requirement 1""
    ""Element 6"" - refines -> ""Requirement 1""
    ""Element 7"" - traces -> ""Requirement 1""", diagram, ignoreLineEndingDifferences: true);
    }
}

