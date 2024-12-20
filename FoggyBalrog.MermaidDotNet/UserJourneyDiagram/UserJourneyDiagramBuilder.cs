﻿using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.UserJourneyDiagram.Model;
using Task = FoggyBalrog.MermaidDotNet.UserJourneyDiagram.Model.Task;

namespace FoggyBalrog.MermaidDotNet.UserJourneyDiagram;

/// <summary>
/// A builder for creating user journey diagrams.
/// </summary>
public class UserJourneyDiagramBuilder
{
    private string _indent = Shared.Indent;
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly bool _isSafe;
    private readonly List<IUserJourneyDiagramItem> _items = [];

    internal UserJourneyDiagramBuilder(string? title, MermaidConfig? config, bool isSafe)
    {
        if (isSafe)
        {
            title.ThrowIfWhiteSpace();
        }

        _title = title;
        _config = config;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds a task to the user journey diagram.
    /// </summary>
    /// <param name="description">The description of the task.</param>
    /// <param name="score">The score of the task.</param>
    /// <param name="actors">A list of actors that are involved in the task.</param>
    /// <returns>The current instance of the <see cref="UserJourneyDiagramBuilder"/>.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> or any of <paramref name="actors"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public UserJourneyDiagramBuilder AddTask(string description, int score, params string[] actors)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
            actors.ThrowIfAnyWhitespace();
        }

        var task = new Task(description, score, actors);
        _items.Add(task);
        return this;
    }

    /// <summary>
    /// Adds a section to the user journey diagram.
    /// </summary>
    /// <param name="description">The description of the section.</param>
    /// <returns>The current instance of the <see cref="UserJourneyDiagramBuilder"/>.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public UserJourneyDiagramBuilder AddSection(string description)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
        }

        var section = new Section(description);
        _items.Add(section);
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the user journey diagram.
    /// </summary>
    /// <returns>The Mermaid code for the user journey diagram.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("journey");

        foreach (IUserJourneyDiagramItem? item in _items)
        {
            switch (item)
            {
                case Task task:
                    string actorsString = task.Actors.Length == 0 ? string.Empty : $": {string.Join(", ", task.Actors)}";
                    builder.AppendLine($"{_indent}{task.Description}: {task.Score}{actorsString}");
                    break;
                case Section section:
                    _indent = Shared.Indent;
                    builder.AppendLine($"{_indent}section {section.Description}");
                    _indent += Shared.Indent;
                    break;
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
