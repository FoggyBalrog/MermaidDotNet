namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

/// <summary>
/// Represents an item that can be linked to another item.
/// </summary>
public interface ILinkable : IFlowItem
{
    public string Id { get; }
}
