namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

/// <summary>
/// Represents a property of a class.
/// </summary>
public record Property
{
    internal Property(string type, string name)
    {
        Type = type;
        Name = name;
    }

    /// <summary>
    /// The type of the property.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// The name of the property.
    /// </summary>
    public string Name { get; }
}
