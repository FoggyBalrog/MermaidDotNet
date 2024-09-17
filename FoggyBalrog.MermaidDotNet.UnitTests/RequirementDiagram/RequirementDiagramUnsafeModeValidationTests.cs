using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.RequirementDiagram;

public class RequirementDiagramUnsafeModeValidationTests
{
    [Fact]
    public void AddRequirement_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddRequirement(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddRequirement_DoesNotThrowIfIdIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddRequirement("Name", out var _, id: " ")
            .Build();
    }

    [Fact]
    public void AddRequirement_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddRequirement("Name", out var _, text: " ")
            .Build();
    }

    [Fact]
    public void AddElement_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddElement(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddElement_DoesNotThrowIfTypeIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddElement("Name", out var _, type: " ")
            .Build();
    }

    [Fact]
    public void AddElement_DoesNotThrowIfDocRefIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddElement("Name", out var _, docRef: " ")
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfSourceIsForeign()
    {
        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddElement("Source", out var source);

        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddElement("Target", out var target)
            .AddRelationship(source, target, RelationshipType.Copies)
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfTargetIsForeign()
    {
        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddElement("Target", out var target);

        Mermaid
            .Unsafe
            .RequirementDiagram()
            .AddElement("Source", out var source)
            .AddRelationship(source, target, RelationshipType.Copies)
            .Build();
    }
}
