namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

/// <summary>
/// Represents a method in a class.
/// </summary>
public class Method
{
    internal Method(string? returnType, string name, Visibilities visibility, List<Parameter> parameters)
    {
        ReturnType = returnType;
        Name = name;
        Visibility = visibility;
        Parameters = parameters;
    }

    /// <summary>
    /// The return type of the method.
    /// </summary>
    public string? ReturnType { get; }

    /// <summary>
    /// The name of the method.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The visibility of the method.
    /// </summary>
    public Visibilities Visibility { get; }

    /// <summary>
    /// The parameters of the method.
    /// </summary>
    public List<Parameter> Parameters { get; }
}
