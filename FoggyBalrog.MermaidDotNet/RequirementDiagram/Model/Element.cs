namespace FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

public record Element : IRequirementNode
{
    internal Element(string name, string? type, string? docRef)
    {
        Name = name;
        Type = type;
        DocRef = docRef;
    }

    public string Name { get; }
    public string? Type { get; }
    public string? DocRef { get; }
}
