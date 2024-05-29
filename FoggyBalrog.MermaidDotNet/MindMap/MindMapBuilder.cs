using System.Text;
using FoggyBalrog.MermaidDotNet.MindMap.Model;

namespace FoggyBalrog.MermaidDotNet.MindMap;

public class MindMapBuilder
{
    private readonly Node _root;
    private const string _defaultIndent = "    ";

    internal MindMapBuilder(string rootText, NodeShape rootShape)
    {
        _root = new Node(rootText, rootShape);
    }

    public MindMapBuilder AddNode(string text, out Node node, Node? parent = null, NodeShape shape = NodeShape.Default)
    {
        node = new Node(text, shape);
        (parent ?? _root).AddChild(node);
        return this;
    }

    public string Build()
    {
        var builder = new StringBuilder();

        builder.AppendLine("mindmap");

        BuildNode(builder, _root);

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private static void BuildNode(StringBuilder builder, Node node, string indent = _defaultIndent, int count = 0)
    {
        if (node.Shape == NodeShape.Default)
        {
            builder.AppendLine($"{indent}{node.Text}");
        }
        else
        {
            string id = $"id{count}";
            (string left, string right) = node.Shape switch
            {
                NodeShape.Square => ("[", "]"),
                NodeShape.RoundedSquare => ("(", ")"),
                NodeShape.Circle => ("((", "))"),
                NodeShape.Bang => ("))", "(("),
                NodeShape.Cloud => (")", "("),
                NodeShape.Hexagon => ("{{", "}}"),
                _ => throw new InvalidOperationException($"Unknown rootShape: {node.Shape}")
            };
            builder.AppendLine($"{indent}{id}{left}{node.Text}{right}");
        }

        foreach (Node? child in node.Children)
        {
            BuildNode(builder, child, indent + _defaultIndent, count + 1);
        }
    }
}
