namespace FoggyBalrog.MermaidDotNet.RequirementDiagram.Model;

/// <summary>
/// Represents a node in the requirement diagram.
/// </summary>
public interface IRequirementNode
{
    /// <summary>
    /// The name of the node.
    /// </summary>
    string Name { get; }
}
