using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.RequirementDiagram;

public class RequirementDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void AddRequirement_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .RequirementDiagram(options: _options)
            .AddRequirement(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddRequirement_DoesNotThrowIfIdIsWhiteSpace()
    {
        Mermaid
            .RequirementDiagram(options: _options)
            .AddRequirement("Name", out var _, id: " ")
            .Build();
    }

    [Fact]
    public void AddRequirement_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .RequirementDiagram(options: _options)
            .AddRequirement("Name", out var _, text: " ")
            .Build();
    }

    [Fact]
    public void AddElement_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .RequirementDiagram(options: _options)
            .AddElement(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddElement_DoesNotThrowIfTypeIsWhiteSpace()
    {
        Mermaid
            .RequirementDiagram(options: _options)
            .AddElement("Name", out var _, type: " ")
            .Build();
    }

    [Fact]
    public void AddElement_DoesNotThrowIfDocRefIsWhiteSpace()
    {
        Mermaid
            .RequirementDiagram(options: _options)
            .AddElement("Name", out var _, docRef: " ")
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfSourceIsForeign()
    {
        Mermaid
            .RequirementDiagram(options: _options)
            .AddElement("Source", out var source);

        Mermaid
            .RequirementDiagram(options: _options)
            .AddElement("Target", out var target)
            .AddRelationship(source, target, RelationshipType.Copies)
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfTargetIsForeign()
    {
        Mermaid
            .RequirementDiagram(options: _options)
            .AddElement("Target", out var target);

        Mermaid
            .RequirementDiagram(options: _options)
            .AddElement("Source", out var source)
            .AddRelationship(source, target, RelationshipType.Copies)
            .Build();
    }
}
