namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

public record EntityAttribute(string Type, string Name, EntityAttributeKeys Keys, string Comment)
{
    public static implicit operator EntityAttribute((string Type, string Name, EntityAttributeKeys Keys, string Comment) tuple)
    {
        return new EntityAttribute(tuple.Type, tuple.Name, tuple.Keys, tuple.Comment);
    }

    public static implicit operator EntityAttribute((string Type, string Name, EntityAttributeKeys Keys) tuple)
    {
        return new EntityAttribute(tuple.Type, tuple.Name, tuple.Keys, string.Empty);
    }

    public static implicit operator EntityAttribute((string Type, string Name, string Comment) tuple)
    {
        return new EntityAttribute(tuple.Type, tuple.Name, EntityAttributeKeys.None, tuple.Comment);
    }

    public static implicit operator EntityAttribute((string Type, string Name) tuple)
    {
        return new EntityAttribute(tuple.Type, tuple.Name, EntityAttributeKeys.None, string.Empty);
    }

    public void Deconstruct(out string type, out string name, out EntityAttributeKeys keys, out string comment)
    {
        type = Type;
        name = Name;
        keys = Keys;
        comment = Comment;
    }
}