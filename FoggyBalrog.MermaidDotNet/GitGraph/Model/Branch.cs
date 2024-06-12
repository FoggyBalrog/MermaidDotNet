namespace FoggyBalrog.MermaidDotNet.GitGraph.Model;

public record Branch : IGitCommand
{
    internal Branch(string name, int? order)
    {
        Name = name;
        Order = order;
    }

    public string Name { get; }
    public int? Order { get; }
}
