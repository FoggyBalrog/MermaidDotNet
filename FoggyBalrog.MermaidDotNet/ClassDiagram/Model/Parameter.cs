namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

public record Parameter
{
    internal Parameter(string type, string name)
    {
        Type = type;
        Name = name;
    }

    public string Type { get; }
    public string Name { get; }
}
