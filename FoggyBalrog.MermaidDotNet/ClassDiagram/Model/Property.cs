namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

public record Property
{
    internal Property(string type, string name)
    {
        Type = type;
        Name = name;
    }

    public string Type { get; }
    public string Name { get; }
}
