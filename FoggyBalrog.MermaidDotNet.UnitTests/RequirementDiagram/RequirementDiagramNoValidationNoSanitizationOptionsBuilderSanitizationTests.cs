using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.RequirementDiagram;

public class RequirementDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .RequirementDiagram(options: _options)
            .AddRequirement("Req\"\\", out Requirement r, id: "ID\"\\", text: "Text\"\\")
            .AddElement("El\"\\", out Element e, type: "Type\"\\", docRef: "Doc\"\\")
            .AddRelationship(r, e, RelationshipType.Contains)
            .Build();

        Assert.Contains("Req\"\\", diagram);
        Assert.Contains("ID\"\\", diagram);
        Assert.Contains("Text\"\\", diagram);
        Assert.Contains("El\"\\", diagram);
        Assert.Contains("Type\"\\", diagram);
        Assert.Contains("Doc\"\\", diagram);
        Assert.DoesNotContain("#34;", diagram);
    }
}
