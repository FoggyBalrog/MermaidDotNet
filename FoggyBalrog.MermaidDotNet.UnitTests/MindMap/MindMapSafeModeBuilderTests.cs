﻿using FoggyBalrog.MermaidDotNet.MindMap.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.MindMap;

public class MindMapSafeModeBuilderTests
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
            .MindMap("Root", rootShape: NodeShape.Hexagon)
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

    [Fact]
    public void CanBuildMindMapWithIconAndClasses()
    {
        string mindMap = Mermaid
            .MindMap("Root", rootIcon: "fa fa-home", rootClasses: ["class1", "class2"])
            .AddNode("Node 1", out Node node1, icon: "fa fa-book", classes: ["class3", "class4"])
            .AddNode("Node 2", out Node _, icon: "fa fa-hat-wizard", classes: ["class5", "class6"], parent: node1)
            .Build();

        Assert.Equal(@"mindmap
    Root
    ::icon(fa fa-home)
    ::: class1 class2
        Node 1
        ::icon(fa fa-book)
        ::: class3 class4
            Node 2
            ::icon(fa fa-hat-wizard)
            ::: class5 class6", mindMap, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildMindMapWithMarkdown()
    {
        string mindMap = Mermaid
            .MindMap("Root", rootIsMarkdown: true, rootShape: NodeShape.Square)
            .AddNode("Node 1", out Node node1, isMarkdown: true, shape: NodeShape.Square)
            .AddNode("Node 2", out Node _, parent: node1, isMarkdown: true, shape: NodeShape.Square)
            .Build();

        Assert.Equal(@"mindmap
    id0[""`Root`""]
        id1[""`Node 1`""]
            id2[""`Node 2`""]", mindMap, ignoreLineEndingDifferences: true);
    }
}
