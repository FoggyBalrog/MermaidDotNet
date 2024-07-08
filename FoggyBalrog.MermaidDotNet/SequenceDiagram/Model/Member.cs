namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

/// <summary>
/// Represents a member of a sequence.
/// </summary>
public record Member
{
    internal Member(string name, MemberType type)
    {
        Name = name;
        Type = type;
    }

    /// <summary>
    /// The name of the member.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The type of the member.
    /// </summary>
    public MemberType Type { get; }
}