namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

public record Cardinality
{
    internal Cardinality(string from, string to)
    {
        From = from;
        To = to;
    }

    public static Cardinality One { get; } = new("1", "1");
    public static Cardinality ZeroOrOne { get; } = new("0", "1");
    public static Cardinality ZeroOrMore { get; } = new("0", "*");
    public static Cardinality OneOrMore { get; } = new("1", "*");
    public static Cardinality Any { get; } = new("*", "*");

    public static Cardinality Exactly(string value) => new(value, value);
    public static Cardinality Exactly(int count) => Exactly(count.ToString());
    public static Cardinality Range(string from, string to) => new(from, to);
    public static Cardinality Range(int from, int to) => Range(from.ToString(), to.ToString());

    public string From { get; }
    public string To { get; }
}
