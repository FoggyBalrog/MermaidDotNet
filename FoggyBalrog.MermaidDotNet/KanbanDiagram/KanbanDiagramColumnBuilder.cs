using FoggyBalrog.MermaidDotNet.KanbanDiagram.Model;

using Task = FoggyBalrog.MermaidDotNet.KanbanDiagram.Model.Task;

namespace FoggyBalrog.MermaidDotNet.KanbanDiagram;

public class KanbanDiagramColumnBuilder
{
    private readonly List<Task> _tasks = [];
    private readonly string _title;
    private readonly MermaidDotNetOptions _options;

    internal KanbanDiagramColumnBuilder(string title, MermaidDotNetOptions? options)
    {
        _title = title;
        _options = options ?? new MermaidDotNetOptions();
    }

    public KanbanDiagramColumnBuilder AddTask(string description, string? assigned = null, string? ticket = null, Priority? priority = null)
    {
        if (_options.SanitizeInputs)
        {
            description = KanbanDiagramSanitizer.SanitizeTaskDescription(description);
            assigned = assigned is null ? null : KanbanDiagramSanitizer.SanitizeAssigned(assigned);
            ticket = ticket is null ? null : KanbanDiagramSanitizer.SanitizeTicket(ticket);
        }

        if (_options.ValidateInputs)
        {
            description.ThrowIfWhiteSpace();
            assigned.ThrowIfWhiteSpace();
            ticket.ThrowIfWhiteSpace();
            KanbanDiagramSanitizer.ValidateTaskDescription(description);

            if (assigned is not null)
            {
                KanbanDiagramSanitizer.ValidateAssigned(assigned);
            }

            if (ticket is not null)
            {
                KanbanDiagramSanitizer.ValidateTicket(ticket);
            }
        }

        _tasks.Add(new Task(description, assigned, ticket, priority));
        return this;
    }

    internal Column ToColumn()
    {
        return new Column(_title, _tasks);
    }
}
