using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.TimelineDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.TimelineDiagram;

/// <summary>
/// A builder for creating timeline diagrams.
/// </summary>
public class TimelineDiagramBuilder
{
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly bool _isSafe;
    private readonly List<ITimelineItem> _items = [];

    internal TimelineDiagramBuilder(string? title, MermaidConfig? config, bool isSafe)
    {
        if (isSafe)
        {
            title?.ThrowIfWhiteSpace();
        }

        _title = title;
        _config = config;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds events to the timeline.
    /// </summary>
    /// <param name="timePeriod">The time period for the events.</param>
    /// <param name="events">The events to add.</param>
    /// <returns>The current instance of the <see cref="TimelineDiagramBuilder"/>.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="timePeriod"/> or any of the <paramref name="events"/> is whitespace, with reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public TimelineDiagramBuilder AddEvents(string timePeriod, params string[] events)
    {
        if (_isSafe)
        {
            timePeriod.ThrowIfWhiteSpace();
            events.ThrowIfAnyWhitespace();
        }

        _items.Add(new TimelineRecord(timePeriod, events));
        return this;
    }

    /// <summary>
    /// Adds a section to the timeline. Events added after this will be in the section, until another section is added.
    /// </summary>
    /// <remarks>
    /// In Mermaid timeline diagram, when adding sections, all events added before the first section are ignored.
    /// </remarks>
    /// <param name="title">The title of the section.</param>
    /// <returns>The current instance of the <see cref="TimelineDiagramBuilder"/>.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public TimelineDiagramBuilder AddSection(string title)
    {
        if (_isSafe)
        {
            title.ThrowIfWhiteSpace();
        }

        _items.Add(new TimelineSection(title));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the timeline diagram.
    /// </summary>
    /// <returns>The Mermaid code for the timeline diagram.</returns>
    public string Build()
    {
        string indent = Shared.Indent;
        var builder = new StringBuilder();

        // Mermaid timeline diagram seems to not support title setting from frontmatter
        builder.Append(FrontmatterGenerator.Generate(null, _config));

        builder.AppendLine("timeline");

        // Mermaid timeline diagram seems to not support title setting from frontmatter
        if (!string.IsNullOrWhiteSpace(_title))
        {
            builder.AppendLine($"{indent}title {_title}");
        }

        foreach (ITimelineItem? item in _items)
        {
            switch (item)
            {
                case TimelineRecord record:
                    builder.Append($"{indent}{record.TimePeriod}");
                    foreach (string @event in record.Events)
                    {
                        builder.Append($" : {@event}");
                    }
                    builder.AppendLine();
                    break;

                case TimelineSection section:
                    indent = Shared.Indent;
                    builder.AppendLine($"{indent}section {section.Title}");
                    indent = Shared.Indent.Repeat(2);
                    break;
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
