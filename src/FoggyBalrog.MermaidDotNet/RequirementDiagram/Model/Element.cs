namespace FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

/// <summary>
/// Represents an element in a requirement diagram.
/// </summary>
public record Element : IRequirementNode
{
    internal Element(string name, string? type, string? docRef)
    {
        Name = name;
        Type = type;
        DocRef = docRef;
    }

    /// <summary>
    /// The name of the element.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The optional type of the element.
    /// </summary>
    public string? Type { get; }

    /// <summary>
    /// The optional document reference of the element.
    /// </summary>
    public string? DocRef { get; }
}
