namespace FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

public record Member
{
    internal Member(string name, MemberType type)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; }
    public MemberType Type { get; }
}