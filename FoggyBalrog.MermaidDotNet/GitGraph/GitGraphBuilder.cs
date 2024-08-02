using System.Text;
using FoggyBalrog.MermaidDotNet.GitGraph.Model;

namespace FoggyBalrog.MermaidDotNet.GitGraph;

/// <summary>
/// A builder for creating a Git graph.
/// </summary>
public class GitGraphBuilder
{
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
        title.ThrowIfWhiteSpace();

        _title = title;
        _parallelCommits = parallelCommits;
        _vertical = vertical;
    }

    /// <summary>
    /// Adds a commit to the graph.
    /// </summary>
    /// <param name="id">An optional identifier for the commit. If not specified, the commit will be assigned an auto-generated identifier.</param>
    /// <param name="type">The type of the commit.</param>
    /// <param name="tag">An optional tag for the commit.</param>
    /// <returns>The current <see cref="GitGraphBuilder"/> instance.</returns>"/>
    /// <exception cref="MermaidException">Thrown when <paramref name="id"/> or <paramref name="tag"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public GitGraphBuilder Commit(string? id = null, CommitType type = CommitType.Normal, string? tag = null)
    {
        id.ThrowIfWhiteSpace();
        tag.ThrowIfWhiteSpace();

        _commands.Add(new Commit(id, type, tag));
        return this;
    }

    /// <summary>
    /// Creates a new branch in the graph.
    /// </summary>
    /// <param name="name">The name of the branch.</param>
    /// <param name="branch">The branch that was created.</param>
    /// <param name="order">An optional order for the branch. If not specified, the branch will be ordered based on the order in which it was created.</param>
    /// <returns>The current <see cref="GitGraphBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public GitGraphBuilder Branch(string name, out Branch branch, int? order = null)
    {
        name.ThrowIfWhiteSpace();

        branch = new Branch(name, order);
        _commands.Add(branch);
        return this;
    }

    /// <summary>
    /// Checks out a branch in the graph.
    /// </summary>
    /// <param name="branch">The branch to check out.</param>
    /// <returns>The current <see cref="GitGraphBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="branch"/> is not part of the current diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public GitGraphBuilder Checkout(Branch branch)
    {
        branch.ThrowIfForeignTo(_commands);

        _commands.Add(new Checkout(branch.Name));
        return this;
    }

    /// <summary>
    /// Checks out the main branch in the graph.
    /// </summary>
    /// <returns>The current <see cref="GitGraphBuilder"/> instance.</returns>
    public GitGraphBuilder CheckoutMain()
    {
        _commands.Add(new Checkout(_mainBranchName));
        return this;
    }

    /// <summary>
    /// Merges a branch into the current branch.
    /// </summary>
    /// <param name="branch">The branch to merge into the current branch.</param>
    /// <param name="id">An optional identifier for the merge commit. If not specified, the merge commit will be assigned an auto-generated identifier.</param>
    /// <param name="type">The type of the merge commit.</param>
    /// <param name="tag">An optional tag for the merge commit.</param>
    /// <returns>The current <see cref="GitGraphBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="branch"/> is not part of the current diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="id"/> or <paramref name="tag"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public GitGraphBuilder Merge(Branch branch, string? id = null, CommitType type = CommitType.Normal, string? tag = null)
    {
        branch.ThrowIfForeignTo(_commands);
        id.ThrowIfWhiteSpace();
        tag.ThrowIfWhiteSpace();

        _commands.Add(new Merge(branch.Name, id, type, tag));
        return this;
    }

    /// <summary>
    /// Builds the Mermaod code for the Git graph.
    /// </summary>
    /// <returns>The Mermaid code for the Git graph.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an unknown command is encountered. Should never happen.</exception>
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
                builder.AppendLine($"{Shared.Indent}gitGraph:");
                builder.AppendLine($"{Shared.Indent.Repeat(2)}parallelCommits: true");
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
                    builder.AppendLine($"{Shared.Indent}branch {branch.Name}{order}");
                    break;

                case Checkout checkout:
                    builder.AppendLine($"{Shared.Indent}checkout {checkout.BranchName}");
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

        builder.AppendLine($"{Shared.Indent}commit{id}{type}{tag}");
    }

    private static void BuildMerge(StringBuilder builder, Merge merge)
    {
        string id = merge.Id is null ? "" : $" id: \"{merge.Id}\"";
        string tag = merge.Tag is null ? "" : $" tag: \"{merge.Tag}\"";
        string type = merge.Type == CommitType.Normal ? "" : $" type: {merge.Type.ToString().ToUpperInvariant()}";

        builder.AppendLine($"{Shared.Indent}merge {merge.BranchName}{id}{type}{tag}");
    }
}
