using System.Drawing;

namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

/// <summary>
/// Represents a box in a sequence diagram.
/// </summary>
public record Box
{
    private readonly List<Member> _members = [];

    internal Box(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    /// <summary>
    /// The name of the box.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The color of the box.
    /// </summary>
    public Color Color { get; }

    /// <summary>
    /// The members of the box.
    /// </summary>
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    internal void AddMember(Member member)
    {
        _members.Add(member);
    }
}
