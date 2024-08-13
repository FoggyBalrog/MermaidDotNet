using System.Text;
using FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.GanttDiagram;

/// <summary>
/// A builder for creating a Gantt diagram.
/// </summary>
public class GanttDiagramBuilder
{
    private string _indent = Shared.Indent;
    private readonly string? _title;
    private readonly bool _compactMode;
    private readonly bool _hideTodayMarker;
    private readonly string _dateFormat;
    private readonly string? _axisFormat;
    private readonly string? _tickInterval;
    private readonly string? _weekIntervalStartDay;
    private readonly List<Exclude> _excludes = [];
    private readonly List<IGanttItem> _items = [];
    private int _taskCounter = 1;

    internal GanttDiagramBuilder(
        string? title,
        bool compactMode,
        bool hideTodayMarker,
        string dateFormat,
        string? axisFormat,
        string? tickInterval,
        string? weekIntervalStartDay)
    {
        title.ThrowIfWhiteSpace();
        dateFormat.ThrowIfWhiteSpace();
        axisFormat.ThrowIfWhiteSpace();
        tickInterval.ThrowIfWhiteSpace();
        weekIntervalStartDay.ThrowIfWhiteSpace();

        _title = title;
        _compactMode = compactMode;
        _hideTodayMarker = hideTodayMarker;
        _dateFormat = dateFormat;
        _axisFormat = axisFormat;
        _tickInterval = tickInterval;
        _weekIntervalStartDay = weekIntervalStartDay;
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
        name.ThrowIfWhiteSpace();

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
        name.ThrowIfWhiteSpace();

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
        name.ThrowIfWhiteSpace();
        afterTask.ThrowIfForeignTo(_items);

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
        name.ThrowIfWhiteSpace();
        afterTask.ThrowIfForeignTo(_items);

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
        name.ThrowIfWhiteSpace();
        untilTask.ThrowIfForeignTo(_items);

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
        name.ThrowIfWhiteSpace();
        afterTask.ThrowIfForeignTo(_items);
        untilTask.ThrowIfForeignTo(_items);

        task = new AfterUntilGanttTask($"task{_taskCounter++}", name, afterTask, untilTask, tags);
        _items.Add(task);
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
        name.ThrowIfWhiteSpace();

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
        task.ThrowIfForeignTo(_items);
        functionName.ThrowIfWhiteSpace();

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
        task.ThrowIfForeignTo(_items);
        uri.ThrowIfWhiteSpace();

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

        if (_compactMode)
        {
            builder
                .AppendLine("---")
                .AppendLine("displayMode: compact")
                .AppendLine("---");
        }

        builder.AppendLine("gantt");

        if (!string.IsNullOrWhiteSpace(_title))
        {
            builder.AppendLine($"{_indent}title {_title}");
        }

        builder.AppendLine($"{_indent}dateFormat {_dateFormat}");

        if (_hideTodayMarker)
        {
            builder.AppendLine($"{_indent}todayMarker off");
        }

        if (!string.IsNullOrWhiteSpace(_axisFormat))
        {
            builder.AppendLine($"{_indent}axisFormat {_axisFormat}");
        }

        if (!string.IsNullOrWhiteSpace(_tickInterval))
        {
            builder.AppendLine($"{_indent}tickInterval {_tickInterval}");
        }

        if (!string.IsNullOrWhiteSpace(_weekIntervalStartDay))
        {
            builder.AppendLine($"{_indent}weekday {_weekIntervalStartDay}");
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
                default:
                    throw new NotSupportedException($"Item type {item.GetType().Name} is not supported.");
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
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
            StartEndGanttTask startEndTask => $"{_indent}{startEndTask.Name}: {tags}{startEndTask.Id}, {DateFormatConverter.ToDayjsFormat(startEndTask.Start, _dateFormat)}, {DateFormatConverter.ToDayjsFormat(startEndTask.End, _dateFormat)}",
            StartDurationGanttTask startDurationTask => $"{_indent}{startDurationTask.Name}: {tags}{startDurationTask.Id}, {DateFormatConverter.ToDayjsFormat(startDurationTask.Start, _dateFormat)}, {startDurationTask.Duration.TotalDays}d",
            AfterEndGanttTask afterEndTask => $"{_indent}{afterEndTask.Name}: {tags}{afterEndTask.Id}, after {afterEndTask.AfterTask.Id}, {DateFormatConverter.ToDayjsFormat(afterEndTask.End, _dateFormat)}",
            AfterDurationGanttTask afterDurationTask => $"{_indent}{afterDurationTask.Name}: {tags}{afterDurationTask.Id}, after {afterDurationTask.AfterTask.Id}, {afterDurationTask.Duration.TotalDays}d",
            StartUntilGanttTask startUntilTask => $"{_indent}{startUntilTask.Name}: {tags}{startUntilTask.Id}, {DateFormatConverter.ToDayjsFormat(startUntilTask.Start, _dateFormat)}, until {startUntilTask.UntilTask.Id}",
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
