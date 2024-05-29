using FoggyBalrog.MermaidDotNet.MindMap.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MindMapBuilderTests
{
    [Fact]
    public void CanBuildSimpleMindMap()
    {
        string mindMap = Mermaid
            .MindMap("Root")
            .Build();

        Assert.Equal(@"mindmap
    Root", mindMap, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildComplexMindMap()
    {
        string mindMap = Mermaid
            .MindMap("Root")
            .AddNode("Node 1", out Node node1)
            .AddNode("Node 2", out Node node2, node1)
            .AddNode("Node 3", out Node node3, node1)
            .AddNode("Node 4", out Node _, node2)
            .AddNode("Node 5", out Node _, node2)
            .AddNode("Node 6", out Node _, node3)
            .AddNode("Node 7", out Node _, node3)
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
        string mindMap = Mermaid
            .MindMap("Root", NodeShape.Hexagon)
            .AddNode("Node 1", out Node node1, shape: NodeShape.Square)
            .AddNode("Node 2", out Node node2, shape: NodeShape.RoundedSquare, parent: node1)
            .AddNode("Node 3", out Node _, shape: NodeShape.Circle, parent: node1)
            .AddNode("Node 4", out Node _, shape: NodeShape.Bang, parent: node2)
            .AddNode("Node 5", out Node _, shape: NodeShape.Cloud, parent: node2)
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
