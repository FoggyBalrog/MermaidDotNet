using System.Text;
using FoggyBalrog.MermaidDotNet.TimelineDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.TimelineDiagram;

public class TimelineDiagramBuilder
{
    private const string _doubleIndent = "        ";
    private const string _singleIndent = "    ";
    private readonly string? _title;
    private readonly List<ITimelineItem> _items = [];

    internal TimelineDiagramBuilder(string? title)
    {
        _title = title;
    }

    public TimelineDiagramBuilder AddEvents(string timePeriod, params string[] events)
    {
        _items.Add(new TimelineRecord(timePeriod, events));
        return this;
    }

    public TimelineDiagramBuilder AddSection(string title)
    {
        _items.Add(new TimelineSection(title));
        return this;
    }

    public string Build()
    {
        string indent = _singleIndent;
        var builder = new StringBuilder();

        builder.AppendLine("timeline");

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
                    indent = _singleIndent;
                    builder.AppendLine($"{indent}section {section.Title}");
                    indent = _doubleIndent;
                    break;
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
