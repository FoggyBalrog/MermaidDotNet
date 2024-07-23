namespace FoggyBalrog.MermaidDotNet.GitGraph.Model;

/// <summary>
/// Represents a branch in a git graph.
/// </summary>
public record Branch : IGitCommand
{
    internal Branch(string name, int? order)
    {
        Name = name;
        Order = order;
    }

    /// <summary>
    /// The name of the branch.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The optional order of the branch.
    /// </summary>
    public int? Order { get; }
}
