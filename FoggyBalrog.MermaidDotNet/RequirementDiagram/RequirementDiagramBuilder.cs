using System.Text;
using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.RequirementDiagram;

public class RequirementDiagramBuilder
{
    private const string _indent = "    ";
    private readonly List<IRequirementNode> _nodes = new();
    private readonly List<Relationship> _relationships = new();

    internal RequirementDiagramBuilder()
    {
    }

    public RequirementDiagramBuilder AddRequirement(
        string name,
        out Requirement requirement,
        string? id = null,
        string? text = null,
        RequirementType type = RequirementType.Default,
        RequirementRisk risk = RequirementRisk.Undefined,
        RequirementVerificationMethod verificationMethod = RequirementVerificationMethod.Undefined)
    {
        requirement = new Requirement(name, id, text, type, risk, verificationMethod);
        _nodes.Add(requirement);
        return this;
    }

    public RequirementDiagramBuilder AddElement(
        string name,
        out Element element,
        string? type = null,
        string? docRef = null)
    {
        element = new Element(name, type, docRef);
        _nodes.Add(element);
        return this;
    }

    public RequirementDiagramBuilder AddRelationship(IRequirementNode source, IRequirementNode target, RelationshipType type)
    {
        _relationships.Add(new Relationship(source, target, type));
        return this;
    }

    public string Build()
    {
        var builder = new StringBuilder();

        builder.AppendLine("requirementDiagram");

        foreach (var node in _nodes)
        {
            if (node is Requirement requirement)
            {
                BuildRequirement(builder, requirement);
            }
            else if (node is Element element)
            {
                BuildElement(builder, element);
            }
        }

        foreach (var relationship in _relationships)
        {
            BuildRelationship(builder, relationship);
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private void BuildRelationship(StringBuilder builder, Relationship relationship)
    {
        builder.AppendLine($"{_indent}\"{relationship.Source.Name}\" - {relationship.Type.ToString().ToLowerInvariant()} -> \"{relationship.Target.Name}\"");
    }

    private void BuildElement(StringBuilder builder, Element element)
    {
        builder.AppendLine($"{_indent}element \"{element.Name}\" {{");

        if (element.Type is not null)
        {
            builder.AppendLine($"{_indent}{_indent}type: \"{element.Type}\"");
        }

        if (element.DocRef is not null)
        {
            builder.AppendLine($"{_indent}{_indent}docRef: \"{element.DocRef}\"");
        }

        builder.AppendLine($"{_indent}}}");
    }

    private void BuildRequirement(StringBuilder builder, Requirement requirement)
    {
        string requirementKeyword = requirement.Type switch
        {
            RequirementType.Default => "requirement",
            _ => $"{requirement.Type.ToString().ToLowerInvariant()}Requirement"
        };

        builder.AppendLine($"{_indent}{requirementKeyword} \"{requirement.Name}\" {{");

        if (requirement.Id is not null)
        {
            builder.AppendLine($"{_indent}{_indent}id: \"{requirement.Id}\"");
        }

        if (requirement.Text is not null)
        {
            builder.AppendLine($"{_indent}{_indent}text: \"{requirement.Text}\"");
        }

        if (requirement.Risk != RequirementRisk.Undefined)
        {
            builder.AppendLine($"{_indent}{_indent}risk: {requirement.Risk}");
        }

        if (requirement.VerificationMethod != RequirementVerificationMethod.Undefined)
        {
            builder.AppendLine($"{_indent}{_indent}verifyMethod: {requirement.VerificationMethod}");
        }

        builder.AppendLine($"{_indent}}}");
    }
}
