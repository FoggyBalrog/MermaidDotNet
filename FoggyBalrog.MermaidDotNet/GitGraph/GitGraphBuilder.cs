using System.Text;
using FoggyBalrog.MermaidDotNet.GitGraph.Model;

namespace FoggyBalrog.MermaidDotNet.GitGraph;

public class GitGraphBuilder
{
    private const string _defaultIndent = "    ";
    private const string _mainBranchName = "main";
    private readonly string? _title;
    private readonly bool _parallelCommits;
    private readonly bool _vertical;
    private readonly List<IGitCommand> _commands = [];

    internal GitGraphBuilder(
        string? title,
        bool parallelCommits,
        bool vertical)
    {
        _title = title;
        _parallelCommits = parallelCommits;
        _vertical = vertical;
    }

    public GitGraphBuilder Commit(string? id = null, CommitType type = CommitType.Normal, string? tag = null)
    {
        _commands.Add(new Commit(id, type, tag));
        return this;
    }

    public GitGraphBuilder Branch(string name, out Branch branch, int? order = null)
    {
        branch = new Branch(name, order);
        _commands.Add(branch);
        return this;
    }

    public GitGraphBuilder Checkout(Branch branch)
    {
        _commands.Add(new Checkout(branch.Name));
        return this;
    }

    public GitGraphBuilder CheckoutMain()
    {
        _commands.Add(new Checkout(_mainBranchName));
        return this;
    }

    public GitGraphBuilder Merge(Branch branch, string? id = null, CommitType type = CommitType.Normal, string? tag = null)
    {
        _commands.Add(new Merge(branch.Name, id, type, tag));
        return this;
    }

    public string Build()
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(_title) || _parallelCommits)
        {
            builder.AppendLine("---");

            if (!string.IsNullOrWhiteSpace(_title))
            {
                builder.AppendLine($"title: {_title}");
            }

            if (_parallelCommits)
            {
                builder.AppendLine("config:");
                builder.AppendLine($"{_defaultIndent}gitGraph:");
                builder.AppendLine($"{_defaultIndent}{_defaultIndent}parallelCommits: true");
            }

            builder.AppendLine("---");
        }

        string orientation = _vertical ? " TB:" : "";

        builder.AppendLine($"gitGraph{orientation}");

        foreach (IGitCommand? command in _commands)
        {
            switch (command)
            {
                case Commit commit:
                    BuildCommit(builder, commit);
                    break;

                case Branch branch:
                    string order = branch.Order is null ? "" : $" order: {branch.Order}";
                    builder.AppendLine($"{_defaultIndent}branch {branch.Name}{order}");
                    break;

                case Checkout checkout:
                    builder.AppendLine($"{_defaultIndent}checkout {checkout.BranchName}");
                    break;

                case Merge merge:
                    BuildMerge(builder, merge);
                    break;

                default:
                    throw new InvalidOperationException($"Unknown command: {command.GetType().Name}");
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private static void BuildCommit(StringBuilder builder, Commit commit)
    {
        string id = commit.Id is null ? "" : $" id: \"{commit.Id}\"";
        string tag = commit.Tag is null ? "" : $" tag: \"{commit.Tag}\"";
        string type = commit.Type == CommitType.Normal ? "" : $" type: {commit.Type.ToString().ToUpperInvariant()}";

        builder.AppendLine($"{_defaultIndent}commit{id}{type}{tag}");
    }

    private static void BuildMerge(StringBuilder builder, Merge merge)
    {
        string id = merge.Id is null ? "" : $" id: \"{merge.Id}\"";
        string tag = merge.Tag is null ? "" : $" tag: \"{merge.Tag}\"";
        string type = merge.Type == CommitType.Normal ? "" : $" type: {merge.Type.ToString().ToUpperInvariant()}";

        builder.AppendLine($"{_defaultIndent}merge {merge.BranchName}{id}{type}{tag}");
    }
}
