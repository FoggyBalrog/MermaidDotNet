using System.Text;
using FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.GanttDiagram;

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
        _title = title;
        _compactMode = compactMode;
        _hideTodayMarker = hideTodayMarker;
        _dateFormat = dateFormat;
        _axisFormat = axisFormat;
        _tickInterval = tickInterval;
        _weekIntervalStartDay = weekIntervalStartDay;
    }

    public GanttDiagramBuilder ExcludeMonday()
    {
        _excludes.Add(Exclude.Monday);
        return this;
    }

    public GanttDiagramBuilder ExcludeTuesday()
    {
        _excludes.Add(Exclude.Tuesday);
        return this;
    }

    public GanttDiagramBuilder ExcludeWednesday()
    {
        _excludes.Add(Exclude.Wednesday);
        return this;
    }

    public GanttDiagramBuilder ExcludeThursday()
    {
        _excludes.Add(Exclude.Thursday);
        return this;
    }

    public GanttDiagramBuilder ExcludeFriday()
    {
        _excludes.Add(Exclude.Friday);
        return this;
    }

    public GanttDiagramBuilder ExcludeSaturday()
    {
        _excludes.Add(Exclude.Saturday);
        return this;
    }

    public GanttDiagramBuilder ExcludeSunday()
    {
        _excludes.Add(Exclude.Sunday);
        return this;
    }

    public GanttDiagramBuilder ExcludeWeekends()
    {
        _excludes.Add(Exclude.Weekends);
        return this;
    }

    public GanttDiagramBuilder ExcludeDates(params DateTimeOffset[] date)
    {
        _excludes.AddRange(date.Select(d => Exclude.Date(d, _dateFormat)));
        return this;
    }

    public GanttDiagramBuilder AddTask(string name, DateTimeOffset start, DateTimeOffset end, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        task = new StartEndGanttTask($"task{_taskCounter++}", name, start, end, tags);
        _items.Add(task);
        return this;
    }

    public GanttDiagramBuilder AddTask(string name, DateTimeOffset start, TimeSpan duration, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        task = new StartDurationGanttTask($"task{_taskCounter++}", name, start, duration, tags);
        _items.Add(task);
        return this;
    }

    public GanttDiagramBuilder AddTask(string name, GanttTask afterTask, DateTimeOffset end, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        task = new AfterEndGanttTask($"task{_taskCounter++}", name, afterTask, end, tags);
        _items.Add(task);
        return this;
    }

    public GanttDiagramBuilder AddTask(string name, GanttTask afterTask, TimeSpan duration, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        task = new AfterDurationGanttTask($"task{_taskCounter++}", name, afterTask, duration, tags);
        _items.Add(task);
        return this;
    }

    public GanttDiagramBuilder AddTask(string name, DateTimeOffset start, GanttTask untilTask, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        task = new StartUntilGanttTask($"task{_taskCounter++}", name, start, untilTask, tags);
        _items.Add(task);
        return this;
    }

    public GanttDiagramBuilder AddTask(string name, GanttTask afterTask, GanttTask untilTask, out GanttTask task, TaskTags tags = TaskTags.None)
    {
        task = new AfterUntilGanttTask($"task{_taskCounter++}", name, afterTask, untilTask, tags);
        _items.Add(task);
        return this;
    }

    public GanttDiagramBuilder AddSection(string name)
    {
        _items.Add(new Section(name));
        return this;
    }

    public GanttDiagramBuilder AddCallback(GanttTask task, string functionName)
    {
        task.ClickBinding = new TaskCallback(functionName);
        return this;
    }

    public GanttDiagramBuilder AddHyperlink(GanttTask task, string uri)
    {
        task.ClickBinding = new TaskHyperlink(uri);
        return this;
    }

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
