using System.Text;
using FoggyBalrog.MermaidDotNet.UserJourneyDiagram.Model;
using Task = FoggyBalrog.MermaidDotNet.UserJourneyDiagram.Model.Task;

namespace FoggyBalrog.MermaidDotNet.UserJourneyDiagram;

public class UserJourneyDiagramBuilder
{
    private string _indent = "    ";
    private readonly string? _title;
    private readonly List<IUserJourneyDiagramItem> _items = [];

    internal UserJourneyDiagramBuilder(string? title)
    {
        _title = title;
    }

    public UserJourneyDiagramBuilder AddTask(string description, int score, params string[] actors)
    {
        var task = new Task(description, score, actors);
        _items.Add(task);
        return this;
    }

    public UserJourneyDiagramBuilder AddSection(string description)
    {
        var section = new Section(description);
        _items.Add(section);
        return this;
    }

    public string Build()
    {
        var builder = new StringBuilder();

        builder.AppendLine("journey");

        if (!string.IsNullOrWhiteSpace(_title))
        {
            builder.AppendLine($"{_indent}title {_title}");
        }

        foreach (IUserJourneyDiagramItem? item in _items)
        {
            switch (item)
            {
                case Task task:
                    string actorsString = task.Actors.Length == 0 ? string.Empty : $": {string.Join(", ", task.Actors)}";
                    builder.AppendLine($"{_indent}{task.Description}: {task.Score}{actorsString}");
                    break;
                case Section section:
                    _indent = "    ";
                    builder.AppendLine($"{_indent}section {section.Description}");
                    _indent += "    ";
                    break;
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
