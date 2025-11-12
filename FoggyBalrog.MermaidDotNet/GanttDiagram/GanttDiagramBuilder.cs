using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.GanttDiagram;

/// <summary>
/// A builder for creating a Gantt diagram.
/// </summary>
public class GanttDiagramBuilder
{
    private string _indent = Shared.Indent;
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly bool _hideTodayMarker;
    private readonly string? _todayMarkerCss;
    private readonly string _dateFormat;
    private readonly bool _isSafe;
    private readonly List<Exclude> _excludes = [];
    private readonly List<IGanttItem> _items = [];
    private int _taskCounter = 1;
    private int _vertCounter = 1;

    internal GanttDiagramBuilder(
        string? title,
        MermaidConfig? config,
        bool hideTodayMarker,
        string? todayMarkerCss,
        string dateFormat,
        bool isSafe)
    {
        if (isSafe)
        {
            title.ThrowIfWhiteSpace();
            dateFormat.ThrowIfWhiteSpace();
            todayMarkerCss.ThrowIfWhiteSpace();

            if (hideTodayMarker && todayMarkerCss is not null)
            {
                throw MermaidException.InvalidConfiguration($"{nameof(hideTodayMarker)} and {nameof(todayMarkerCss)} cannot be used together.");
            }
        }

        _title = title;
        _config = config;
        _hideTodayMarker = hideTodayMarker;
        _todayMarkerCss = todayMarkerCss;
        _dateFormat = dateFormat;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Excludes Monday from the Gantt diagram.
    /// </summary>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeMonday()
    {
        _excludes.Add(Exclude.Monday);
        return this;
    }

    /// <summary>
    /// Excludes Tuesday from the Gantt diagram.
    /// </summary>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeTuesday()
    {
        _excludes.Add(Exclude.Tuesday);
        return this;
    }

    /// <summary>
    /// Excludes Wednesday from the Gantt diagram.
    /// </summary>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeWednesday()
    {
        _excludes.Add(Exclude.Wednesday);
        return this;
    }

    /// <summary>
    /// Excludes Thursday from the Gantt diagram.
    /// </summary>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeThursday()
    {
        _excludes.Add(Exclude.Thursday);
        return this;
    }

    /// <summary>
    /// Excludes Friday from the Gantt diagram.
    /// </summary>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeFriday()
    {
        _excludes.Add(Exclude.Friday);
        return this;
    }

    /// <summary>
    /// Excludes Saturday from the Gantt diagram.
    /// </summary>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeSaturday()
    {
        _excludes.Add(Exclude.Saturday);
        return this;
    }

    /// <summary>
    /// Excludes Sunday from the Gantt diagram.
    /// </summary>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeSunday()
    {
        _excludes.Add(Exclude.Sunday);
        return this;
    }

    /// <summary>
    /// Excludes weekends from the Gantt diagram.
    /// </summary>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeWeekends()
    {
        _excludes.Add(Exclude.Weekends);
        return this;
    }

    /// <summary>
    /// Excludes the specified dates from the Gantt diagram.
    /// </summary>
    /// <param name="dates">One or more dates to exclude.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    public GanttDiagramBuilder ExcludeDates(params DateTimeOffset[] dates)
    {
        _excludes.AddRange(dates.Select(d => Exclude.Date(d, _dateFormat)));
        return this;
    }

    /// <summary>
    /// Adds a task to the Gantt diagram, by specifying the start and end dates.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="start">The start date of the task.</param>
    /// <param name="end">The end date of the task.</param>
    /// <param name="task">The created task.</param>
    /// <param name="tags">The tags of the task.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public GanttDiagramBuilder AddTask(string name, DateTimeOffset start, DateTimeOffset end, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
        }

        task = new StartEndGanttTask($"task{_taskCounter++}", name, start, end, tags);
        _items.Add(task);
        return this;
    }

    /// <summary>
    /// Adds a task to the Gantt diagram, by specifying the start date and duration.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="start">The start date of the task.</param>
    /// <param name="duration">The duration of the task.</param>
    /// <param name="task">The created task.</param>
    /// <param name="tags">The tags of the task.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public GanttDiagramBuilder AddTask(string name, DateTimeOffset start, TimeSpan duration, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
        }

        task = new StartDurationGanttTask($"task{_taskCounter++}", name, start, duration, tags);
        _items.Add(task);
        return this;
    }

    /// <summary>
    /// Adds a task to the Gantt diagram, by specifying a task that it should start after, and the end date.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="afterTask">The task that this task should start after.</param>
    /// <param name="end">The end date of the task.</param>
    /// <param name="task">The created task.</param>
    /// <param name="tags">The tags of the task.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="afterTask"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public GanttDiagramBuilder AddTask(string name, GanttTask afterTask, DateTimeOffset end, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
            afterTask.ThrowIfForeignTo(_items);
        }

        task = new AfterEndGanttTask($"task{_taskCounter++}", name, afterTask, end, tags);
        _items.Add(task);
        return this;
    }

    /// <summary>
    /// Adds a task to the Gantt diagram, by specifying a task that it should start after, and the duration.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="afterTask">The task that this task should start after.</param>
    /// <param name="duration">The duration of the task.</param>
    /// <param name="task">The created task.</param>
    /// <param name="tags">The tags of the task.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="afterTask"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public GanttDiagramBuilder AddTask(string name, GanttTask afterTask, TimeSpan duration, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
            afterTask.ThrowIfForeignTo(_items);
        }

        task = new AfterDurationGanttTask($"task{_taskCounter++}", name, afterTask, duration, tags);
        _items.Add(task);
        return this;
    }

    /// <summary>
    /// Adds a task to the Gantt diagram, by specifying the start date and a task that it should end until.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="start">The start date of the task.</param>
    /// <param name="untilTask">The task that this task should end until.</param>
    /// <param name="task">The created task.</param>
    /// <param name="tags">The tags of the task.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="untilTask"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public GanttDiagramBuilder AddTask(string name, DateTimeOffset start, GanttTask untilTask, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
            untilTask.ThrowIfForeignTo(_items);
        }

        task = new StartUntilGanttTask($"task{_taskCounter++}", name, start, untilTask, tags);
        _items.Add(task);
        return this;
    }

    /// <summary>
    /// Adds a task to the Gantt diagram, by specifying a task that it should start after, and a task that it should end until.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="afterTask">The task that this task should start after.</param>
    /// <param name="untilTask">The task that this task should end until.</param>
    /// <param name="task">The created task.</param>
    /// <param name="tags">The tags of the task.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="afterTask"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="untilTask"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public GanttDiagramBuilder AddTask(string name, GanttTask afterTask, GanttTask untilTask, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
            afterTask.ThrowIfForeignTo(_items);
            untilTask.ThrowIfForeignTo(_items);
        }

        task = new AfterUntilGanttTask($"task{_taskCounter++}", name, afterTask, untilTask, tags);
        _items.Add(task);
        return this;
    }

    /// <summary>
    /// Adds a vertical marker to the gantt diagram, at a given position.
    /// </summary>
    /// <param name="name">The marker name.</param>
    /// <param name="position">The position.</param>
    /// <param name="nextTaskOffset">An optional offset for the following task start.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <remarks>This feature was introduced in Mermaid 11.7.0.</remarks>
    public GanttDiagramBuilder AddVerticalMarker(string name, DateTimeOffset position, TimeSpan? nextTaskOffset = null)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
        }

        _items.Add(new VerticalMarker($"vert{_vertCounter++}", name, position, nextTaskOffset ?? TimeSpan.Zero));
        return this;
    }

    /// <summary>
    /// Adds a section to the Gantt diagram.
    /// </summary>
    /// <param name="name">The name of the section.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public GanttDiagramBuilder AddSection(string name)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
        }

        _items.Add(new Section(name));
        return this;
    }

    /// <summary>
    /// Adds a callback to a task in the Gantt diagram.
    /// </summary>
    /// <param name="task">The task to add the callback to.</param>
    /// <param name="functionName">The name of the function to call when the task is clicked.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="task"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="functionName"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public GanttDiagramBuilder AddCallback(GanttTask task, string functionName)
    {
        if (_isSafe)
        {
            task.ThrowIfForeignTo(_items);
            functionName.ThrowIfWhiteSpace();
        }

        task.ClickBinding = new TaskCallback(functionName);
        return this;
    }

    /// <summary>
    /// Adds a hyperlink to a task in the Gantt diagram.
    /// </summary>
    /// <param name="task">The task to add the hyperlink to.</param>
    /// <param name="uri">The URI to navigate to when the task is clicked.</param>
    /// <returns>The current <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="task"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="uri"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public GanttDiagramBuilder AddHyperlink(GanttTask task, string uri)
    {
        if (_isSafe)
        {
            task.ThrowIfForeignTo(_items);
            uri.ThrowIfWhiteSpace();
        }

        task.ClickBinding = new TaskHyperlink(uri);
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the Gantt diagram.
    /// </summary>
    /// <returns>The Mermaid code for the Gantt diagram.</returns>
    /// <exception cref="NotSupportedException">Thrown when an item type is not supported. Should never happen.</exception>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("gantt");

        builder.AppendLine($"{_indent}dateFormat {_dateFormat}");

        if (_hideTodayMarker)
        {
            builder.AppendLine($"{_indent}todayMarker off");
        }

        if (_todayMarkerCss is not null)
        {
            builder.AppendLine($"{_indent}todayMarker {_todayMarkerCss}");
        }

        if (_excludes.Count > 0)
        {
            builder.AppendLine($"{_indent}excludes {string.Join(", ", _excludes.Select(e => e.Text))}");
        }

        foreach (IGanttItem? item in _items)
        {
            switch (item)
            {
                case Section section:
                    _indent = Shared.Indent;
                    builder.AppendLine($"{_indent}section {section.Name}");
                    _indent += Shared.Indent;
                    break;
                case GanttTask task:
                    BuildTask(builder, task);
                    break;
                case VerticalMarker marker:
                    BuildVerticalMarker(builder, marker);
                    break;
                default:
                    throw new NotSupportedException($"Item type {item.GetType().Name} is not supported.");
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private void BuildVerticalMarker(StringBuilder builder, VerticalMarker marker)
    {
        builder.AppendLine($"{_indent}{marker.Name}: vert, {marker.Id}, {DayjsFormatConverter.FormatDateTimeOffset(marker.Position, _dateFormat)}, {DayjsFormatConverter.FormatTimeSpan(marker.NextTaskOffset)}");
    }

    private void BuildTask(StringBuilder builder, GanttTask task)
    {
        string tags = string.Empty;

        if (task.Tags.HasFlag(TaskTags.Active))
        {
            tags += "active, ";
        }

        if (task.Tags.HasFlag(TaskTags.Done))
        {
            tags += "done, ";
        }

        if (task.Tags.HasFlag(TaskTags.Critical))
        {
            tags += "crit, ";
        }

        if (task.Tags.HasFlag(TaskTags.Milestone))
        {
            tags += "milestone, ";
        }

        string declaration = task switch
        {
            StartEndGanttTask startEndTask => $"{_indent}{startEndTask.Name}: {tags}{startEndTask.Id}, {DayjsFormatConverter.FormatDateTimeOffset(startEndTask.Start, _dateFormat)}, {DayjsFormatConverter.FormatDateTimeOffset(startEndTask.End, _dateFormat)}",
            StartDurationGanttTask startDurationTask => $"{_indent}{startDurationTask.Name}: {tags}{startDurationTask.Id}, {DayjsFormatConverter.FormatDateTimeOffset(startDurationTask.Start, _dateFormat)}, {DayjsFormatConverter.FormatTimeSpan(startDurationTask.Duration)}",
            AfterEndGanttTask afterEndTask => $"{_indent}{afterEndTask.Name}: {tags}{afterEndTask.Id}, after {afterEndTask.AfterTask.Id}, {DayjsFormatConverter.FormatDateTimeOffset(afterEndTask.End, _dateFormat)}",
            AfterDurationGanttTask afterDurationTask => $"{_indent}{afterDurationTask.Name}: {tags}{afterDurationTask.Id}, after {afterDurationTask.AfterTask.Id}, {DayjsFormatConverter.FormatTimeSpan(afterDurationTask.Duration)}",
            StartUntilGanttTask startUntilTask => $"{_indent}{startUntilTask.Name}: {tags}{startUntilTask.Id}, {DayjsFormatConverter.FormatDateTimeOffset(startUntilTask.Start, _dateFormat)}, until {startUntilTask.UntilTask.Id}",
            AfterUntilGanttTask afterUntilTask => $"{_indent}{afterUntilTask.Name}: {tags}{afterUntilTask.Id}, after {afterUntilTask.AfterTask.Id}, until {afterUntilTask.UntilTask.Id}",
            _ => throw new NotSupportedException($"Task type {task.GetType().Name} is not supported.")
        };

        builder.AppendLine(declaration);

        if (task.ClickBinding is not null)
        {
            switch (task.ClickBinding)
            {
                case TaskCallback callback:
                    builder.AppendLine($"{_indent}click {task.Id} call {callback.FunctionName}()");
                    break;

                case TaskHyperlink hyperlink:
                    builder.AppendLine($"{_indent}click {task.Id} href \"{hyperlink.Uri}\"");
                    break;
            }
        }
    }
}
