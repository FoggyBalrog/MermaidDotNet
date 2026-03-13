using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.RequirementDiagram;

public class RequirementDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .RequirementDiagram(options: _options)
            .AddRequirement("Req\"\\", out Requirement r, id: "ID\"\\", text: "Text\"\\")
            .AddElement("El\"\\", out Element e, type: "Type\"\\", docRef: "Doc\"\\")
            .AddRelationship(r, e, RelationshipType.Contains)
            .Build();

        Assert.Contains("Req#34;#92;", diagram);
        Assert.Contains("ID#34;#92;", diagram);
        Assert.Contains("Text#34;#92;", diagram);
        Assert.Contains("El#34;#92;", diagram);
        Assert.Contains("Type#34;#92;", diagram);
        Assert.Contains("Doc#34;#92;", diagram);
    }
}
