using FoggyBalrog.MermaidDotNet.MindMap.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MindMapBuilderTests
{
    [Fact]
    public void CanBuildSimpleMindMap()
    {
        var mindMap = Mermaid
            .MindMap("Root")
            .Build();

        Assert.Equal(@"mindmap
    Root", mindMap, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildComplexMindMap()
    {
        var mindMap = Mermaid
            .MindMap("Root")
            .AddNode("Node 1", out var node1)
            .AddNode("Node 2", out var node2, node1)
            .AddNode("Node 3", out var node3, node1)
            .AddNode("Node 4", out var node4, node2)
            .AddNode("Node 5", out var node5, node2)
            .AddNode("Node 6", out var node6, node3)
            .AddNode("Node 7", out var node7, node3)
            .Build();

        Assert.Equal(@"mindmap
    Root
        Node 1
            Node 2
                Node 4
                Node 5
            Node 3
                Node 6
                Node 7", mindMap, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildMindMapWithDifferentShapes()
    {
        var mindMap = Mermaid
            .MindMap("Root", NodeShape.Hegaxon)
            .AddNode("Node 1", out var node1, shape: NodeShape.Square)
            .AddNode("Node 2", out var node2, shape: NodeShape.RoundedSquare, parent: node1)
            .AddNode("Node 3", out var node3, shape: NodeShape.Circle, parent: node1)
            .AddNode("Node 4", out var node4, shape: NodeShape.Bang, parent: node2)
            .AddNode("Node 5", out var node5, shape: NodeShape.Cloud, parent: node2)
            .Build();

        Assert.Equal(@"mindmap
    id0{{Root}}
        id1[Node 1]
            id2(Node 2)
                id3))Node 4((
                id3)Node 5(
            id2((Node 3))", mindMap, ignoreLineEndingDifferences: true);
    }
}
