namespace FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

public record Requirement : IRequirementNode
{
    internal Requirement(string name, string? id, string? text, RequirementType type, RequirementRisk risk, RequirementVerificationMethod verificationMethod)
    {
        Id = id;
        Name = name;
        Text = text;
        Type = type;
        Risk = risk;
        VerificationMethod = verificationMethod;
    }

    public string Name { get; }
    public string? Id { get; }
    public string? Text { get; }
    public RequirementType Type { get; }
    public RequirementRisk Risk { get; }
    public RequirementVerificationMethod VerificationMethod { get; }
}
