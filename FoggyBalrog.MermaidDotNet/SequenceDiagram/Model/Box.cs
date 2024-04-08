using System.Drawing;

namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

public record Box
{
    internal Box(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    public List<Member> Members { get; } = [];
    public string Name { get; }
    public Color Color { get; }
}
