﻿using System.Text;
using FoggyBalrog.MermaidDotNet.MindMap.Model;

namespace FoggyBalrog.MermaidDotNet.MindMap;

/// <summary>
/// A builder for creating a mind map diagram.
/// </summary>
public class MindMapBuilder
{
    private readonly Node _root;

    internal MindMapBuilder(string rootText, NodeShape rootShape)
    {
        _root = new Node(rootText, rootShape);
    }

    /// <summary>
    /// Adds a node to the mind map.
    /// </summary>
    /// <param name="text">The text of the node.</param>
    /// <param name="node">The node that was created.</param>
    /// <param name="parent">The parent node of the node. If not specified, the node will be added to the root.</param>
    /// <param name="shape">The shape of the node.</param>
    /// <returns>The current <see cref="MindMapBuilder"/> instance.</returns>
    public MindMapBuilder AddNode(string text, out Node node, Node? parent = null, NodeShape shape = NodeShape.Default)
    {
        node = new Node(text, shape);
        (parent ?? _root).AddChild(node);
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the mind map.
    /// </summary>
    /// <returns>The Mermaid code for the mind map.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.AppendLine("mindmap");

        BuildNode(builder, _root, Shared.Indent);

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private static void BuildNode(StringBuilder builder, Node node, string indent, int count = 0)
    {
        if (node.Shape == NodeShape.Default)
        {
            builder.AppendLine($"{indent}{node.Text}");
        }
        else
        {
            string id = $"id{count}";
            (string left, string right) = SymbolMaps.Nodes[node.Shape];
            builder.AppendLine($"{indent}{id}{left}{node.Text}{right}");
        }

        foreach (Node? child in node.Children)
        {
            BuildNode(builder, child, indent + Shared.Indent, count + 1);
        }
    }
}
