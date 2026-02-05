using FoggyBalrog.MermaidDotNet.KanbanDiagram.Model;

using Task = FoggyBalrog.MermaidDotNet.KanbanDiagram.Model.Task;

namespace FoggyBalrog.MermaidDotNet.KanbanDiagram;

public class KanbanDiagramColumnBuilder
{
    private readonly List<Task> _tasks = [];
    private readonly string _title;

    internal KanbanDiagramColumnBuilder(string title)
    {
        _title = title;
    }

    public KanbanDiagramColumnBuilder AddTask(string description, string? assigned = null, string? ticket = null, Priority? priority = null)
    {
        _tasks.Add(new Task(description, assigned, ticket, priority));
        return this;
    }

    internal Column ToColumn()
    {
        return new Column(_title, _tasks);
    }
}
