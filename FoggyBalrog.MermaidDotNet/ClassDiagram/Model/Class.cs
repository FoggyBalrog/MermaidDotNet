namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

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

    public string Name { get; }
    public string? Label { get; }
    public string? Annotation { get; }
    internal IClassClickBinding? ClickBinding { get; set; }

    public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();
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
