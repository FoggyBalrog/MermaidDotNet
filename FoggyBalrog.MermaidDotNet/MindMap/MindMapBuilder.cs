﻿using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.MindMap.Model;

namespace FoggyBalrog.MermaidDotNet.MindMap;

/// <summary>
/// A builder for creating a mind map diagram.
/// </summary>
public class MindMapBuilder
{
    private readonly Node _root;

    private readonly HashSet<Node> _nodes;
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly bool _isSafe;

    internal MindMapBuilder(
        string rootText,
        string? title,
        MermaidConfig? config,
        NodeShape rootShape,
        bool rootIsMarkdown,
        string? rootIcon,
        string[]? rootClasses,
        bool isSafe)
    {
        if (isSafe)
        {
            rootText.ThrowIfWhiteSpace();

            if (rootIsMarkdown && rootShape == NodeShape.Default)
            {
                throw MermaidException.InvalidOperation("Markdown nodes with default shape are not supported by Mermaid.");
            }
        }

        _root = new Node(rootText, rootShape, rootIsMarkdown, rootIcon, rootClasses);
        _nodes = [_root];
        _title = title;
        _config = config;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds a node to the mind map.
    /// </summary>
    /// <param name="text">The text of the node.</param>
    /// <param name="node">The node that was created.</param>
    /// <param name="parent">The parent node of the node. If not specified, the node will be added to the root.</param>
    /// <param name="shape">The shape of the node.</param>
    /// <param name="icon">The optional font icon of the node.</param>
    /// <param name="classes">The optional CSS classes of the node.</param>
    /// <returns>The current <see cref="MindMapBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="parent"/> is not null and not part of the mind map, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public MindMapBuilder AddNode(
        string text,
        out Node node,
        Node? parent = null,
        NodeShape shape = NodeShape.Default,
        bool isMarkdown = false,
        string? icon = null,
        string[]? classes = null)
    {
        if (_isSafe)
        {
            text.ThrowIfWhiteSpace();
            parent?.ThrowIfForeignTo(_nodes);
            icon?.ThrowIfWhiteSpace();
            classes?.ThrowIfAnyWhitespace();

            if (isMarkdown && shape == NodeShape.Default)
            {
                throw MermaidException.InvalidOperation("Markdown nodes with default shape are not supported by Mermaid.");
            }
        }

        node = new Node(text, shape, isMarkdown, icon, classes ?? []);
        (parent ?? _root).AddChild(node);
        _nodes.Add(node);
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the mind map.
    /// </summary>
    /// <returns>The Mermaid code for the mind map.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("mindmap");

        BuildNode(builder, _root, Shared.Indent);

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private static void BuildNode(StringBuilder builder, Node node, string indent, int count = 0)
    {
        if (node.Shape == NodeShape.Default && !node.IsMarkdown)
        {
            builder.AppendLine($"{indent}{node.Text}");
        }
        else
        {
            string id = $"id{count}";
            (string left, string right) = SymbolMaps.Nodes[node.Shape];
            (string markdownLeft, string markdownRight) = node.IsMarkdown ? ("\"`", "`\"") : ("", "");
            builder.AppendLine($"{indent}{id}{left}{markdownLeft}{node.Text}{markdownRight}{right}");
        }

        if (node.Icon is not null)
        {
            builder.AppendLine($"{indent}::icon({node.Icon})");
        }

        if (node.Classes is not null && node.Classes.Length > 0)
        {
            builder.AppendLine($"{indent}::: {string.Join(" ", node.Classes)}");
        }

        foreach (Node? child in node.Children)
        {
            BuildNode(builder, child, indent + Shared.Indent, count + 1);
        }
    }
}
