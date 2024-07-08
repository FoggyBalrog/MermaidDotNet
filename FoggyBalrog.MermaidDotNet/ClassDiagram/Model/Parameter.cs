namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

/// <summary>
/// Represents a parameter of a method.
/// </summary>
public record Parameter
{
    internal Parameter(string type, string name)
    {
        Type = type;
        Name = name;
    }

    /// <summary>
    /// The type of the parameter.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// The name of the parameter.
    /// </summary>
    public string Name { get; }
}
