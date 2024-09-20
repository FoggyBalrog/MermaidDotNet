﻿namespace FoggyBalrog.MermaidDotNet.UnitTests.MindMap;

public class MindMapUnsafeModeValidationTests
{
    [Fact]
    public void MindMapBuilder_DoesNotThrowIfRootTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .MindMap(" ")
            .Build();
    }

    [Fact]
    public void AddNode_DoesNotThrowIfNodeTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .MindMap("Root")
            .AddNode(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddNode_DoesNotThrowIfParentIsForeign()
    {
        Mermaid
            .Unsafe
            .MindMap("Root")
            .AddNode("Node 1", out var node1);

        Mermaid
            .Unsafe
            .MindMap("Root")
            .AddNode("Node 2", out var node2, node1)
            .Build();
    }
}
