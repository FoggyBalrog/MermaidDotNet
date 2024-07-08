namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

/// <summary>
/// Represents the cardinality of a relationship between two classes.
/// </summary>
public record Cardinality
{
    internal Cardinality(string from, string to)
    {
        From = from;
        To = to;
    }

    /// <summary>
    /// A cardinality of exactly one.
    /// </summary>
    public static Cardinality One { get; } = new("1", "1");

    /// <summary>
    /// A cardinality of zero or one.
    /// </summary>
    public static Cardinality ZeroOrOne { get; } = new("0", "1");

    /// <summary>
    /// A cardinality of zero or more.
    /// </summary>
    public static Cardinality ZeroOrMore { get; } = new("0", "*");

    /// <summary>
    /// A cardinality of one or more.
    /// </summary>
    public static Cardinality OneOrMore { get; } = new("1", "*");

    /// <summary>
    /// A cardinality of any number.
    /// </summary>
    public static Cardinality Any { get; } = new("*", "*");

    /// <summary>
    /// Gets a cardinality of exactly the specified value.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>The cardinality.</returns>
    public static Cardinality Exactly(string value) => new(value, value);

    /// <summary>
    /// Gets a cardinality of exactly the specified count.
    /// </summary>
    /// <param name="count">A count.</param>
    /// <returns>The cardinality.</returns>
    public static Cardinality Exactly(int count) => Exactly(count.ToString());

    /// <summary>
    /// Gets a ranged cardinality.
    /// </summary>
    /// <param name="from">The lower bound of the range.</param>
    /// <param name="to">The upper bound of the range.</param>
    /// <returns>The cardinality.</returns>
    public static Cardinality Range(string from, string to) => new(from, to);

    /// <summary>
    /// Gets a ranged cardinality.
    /// </summary>
    /// <param name="from">The lower bound of the range.</param>
    /// <param name="to">The upper bound of the range.</param>
    /// <returns>The cardinality.</returns>
    public static Cardinality Range(int from, int to) => Range(from.ToString(), to.ToString());

    /// <summary>
    /// The cardinality lower bound.
    /// </summary>
    public string From { get; }

    /// <summary>
    /// The cardinality upper bound.
    /// </summary>
    public string To { get; }
}
