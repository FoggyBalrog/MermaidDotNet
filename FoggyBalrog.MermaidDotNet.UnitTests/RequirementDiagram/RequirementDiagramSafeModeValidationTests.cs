using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.RequirementDiagram;

public class RequirementDiagramSafeModeValidationTests
{
    [Fact]
    public void AddRequirement_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .RequirementDiagram()
                .AddRequirement(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddRequirement_ThrowsIfIdIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .RequirementDiagram()
                .AddRequirement("Name", out var _, id: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddRequirement_ThrowsIfTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .RequirementDiagram()
                .AddRequirement("Name", out var _, text: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddElement_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .RequirementDiagram()
                .AddElement(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddElement_ThrowsIfTypeIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .RequirementDiagram()
                .AddElement("Name", out var _, type: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddElement_ThrowsIfDocRefIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .RequirementDiagram()
                .AddElement("Name", out var _, docRef: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddRelationship_ThrowsIfSourceIsForeign()
    {
        Mermaid
            .RequirementDiagram()
            .AddElement("Source", out var source);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .RequirementDiagram()
                .AddElement("Target", out var target)
                .AddRelationship(source, target, RelationshipType.Copies);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddRelationship_ThrowsIfTargetIsForeign()
    {
        Mermaid
            .RequirementDiagram()
            .AddElement("Target", out var target);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .RequirementDiagram()
                .AddElement("Source", out var source)
                .AddRelationship(source, target, RelationshipType.Copies);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }
}
