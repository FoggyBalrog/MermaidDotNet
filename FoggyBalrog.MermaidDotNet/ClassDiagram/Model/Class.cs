namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

/// <summary>
/// Represents a class in a class diagram.
/// </summary>
public record Class : IClassDiagramItem
{
    private readonly List<Property> _properties = [];
    private readonly List<Method> _methods = [];

    internal Class(string name, string? label, string? annotation, IClassClickBinding? clickBinding)
    {
        Name = name;
        Label = label;
        Annotation = annotation;
        ClickBinding = clickBinding;
    }

    /// <summary>
    /// The name of the class.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// An optional label.
    /// </summary>
    public string? Label { get; }

    /// <summary>
    /// An optional annotation.
    /// </summary>
    public string? Annotation { get; }

    /// <summary>
    /// An optional click binding, that can be either a URL or a JavaScript function.
    /// </summary>
    internal IClassClickBinding? ClickBinding { get; set; }

    /// <summary>
    /// The properties of the class.
    /// </summary>
    public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();

    /// <summary>
    /// The methods of the class.
    /// </summary>
    public IReadOnlyCollection<Method> Methods => _methods.AsReadOnly();

    internal void AddProperty(Property property)
    {
        _properties.Add(property);
    }

    internal void AddMethod(Method method)
    {
        _methods.Add(method);
    }
}
