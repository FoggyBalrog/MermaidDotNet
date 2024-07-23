using System.Text;
using FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.RequirementDiagram;

/// <summary>
/// A builder for requirement diagrams.
/// </summary>
public class RequirementDiagramBuilder
{
    private readonly List<IRequirementNode> _nodes = [];
    private readonly List<Relationship> _relationships = [];

    internal RequirementDiagramBuilder()
    {
    }

    /// <summary>
    /// Adds a requirement to the diagram.
    /// </summary>
    /// <param name="name">The name of the requirement.</param>
    /// <param name="requirement">The requirement that was created.</param>
    /// <param name="id">An optional identifier for the requirement.</param>
    /// <param name="text">An optional text for the requirement.</param>
    /// <param name="type">The type of the requirement.</param>
    /// <param name="risk">The risk of the requirement.</param>
    /// <param name="verificationMethod">The verification method of the requirement.</param>
    /// <returns>The current <see cref="RequirementDiagramBuilder"/> instance.</returns>
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

    /// <summary>
    /// Adds an element to the diagram.
    /// </summary>
    /// <param name="name">The name of the element.</param>
    /// <param name="element">The element that was created.</param>
    /// <param name="type">An optional type for the element.</param>
    /// <param name="docRef">An optional documentation reference for the element.</param>
    /// <returns>The current <see cref="RequirementDiagramBuilder"/> instance.</returns>
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

    /// <summary>
    /// Adds a relationship between two nodes in the diagram.
    /// </summary>
    /// <param name="source">The source node of the relationship.</param>
    /// <param name="target">The target node of the relationship.</param>
    /// <param name="type">The type of the relationship.</param>
    /// <returns>The current <see cref="RequirementDiagramBuilder"/> instance.</returns>
    public RequirementDiagramBuilder AddRelationship(IRequirementNode source, IRequirementNode target, RelationshipType type)
    {
        _relationships.Add(new Relationship(source, target, type));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the requirement diagram.
    /// </summary>
    /// <returns></returns>
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
        builder.AppendLine($"{Shared.Indent}\"{relationship.Source.Name}\" - {relationship.Type.ToString().ToLowerInvariant()} -> \"{relationship.Target.Name}\"");
    }

    private void BuildElement(StringBuilder builder, Element element)
    {
        builder.AppendLine($"{Shared.Indent}element \"{element.Name}\" {{");

        if (element.Type is not null)
        {
            builder.AppendLine($"{Shared.Indent.Repeat(2)}type: \"{element.Type}\"");
        }

        if (element.DocRef is not null)
        {
            builder.AppendLine($"{Shared.Indent.Repeat(2)}docRef: \"{element.DocRef}\"");
        }

        builder.AppendLine($"{Shared.Indent}}}");
    }

    private void BuildRequirement(StringBuilder builder, Requirement requirement)
    {
        string requirementKeyword = requirement.Type switch
        {
            RequirementType.Default => "requirement",
            _ => $"{requirement.Type.ToString().ToLowerInvariant()}Requirement"
        };

        builder.AppendLine($"{Shared.Indent}{requirementKeyword} \"{requirement.Name}\" {{");

        if (requirement.Id is not null)
        {
            builder.AppendLine($"{Shared.Indent.Repeat(2)}id: \"{requirement.Id}\"");
        }

        if (requirement.Text is not null)
        {
            builder.AppendLine($"{Shared.Indent.Repeat(2)}text: \"{requirement.Text}\"");
        }

        if (requirement.Risk != RequirementRisk.Undefined)
        {
            builder.AppendLine($"{Shared.Indent.Repeat(2)}risk: {requirement.Risk}");
        }

        if (requirement.VerificationMethod != RequirementVerificationMethod.Undefined)
        {
            builder.AppendLine($"{Shared.Indent.Repeat(2)}verifyMethod: {requirement.VerificationMethod}");
        }

        builder.AppendLine($"{Shared.Indent}}}");
    }
}
