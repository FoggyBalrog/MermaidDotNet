namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

public class Method
{
    internal Method(string? returnType, string name, Visibilities visibility, List<Parameter> parameters)
    {
        ReturnType = returnType;
        Name = name;
        Visibility = visibility;
        Parameters = parameters;
    }

    public string? ReturnType { get; }
    public string Name { get; }
    public Visibilities Visibility { get; }
    public List<Parameter> Parameters { get; }
}
