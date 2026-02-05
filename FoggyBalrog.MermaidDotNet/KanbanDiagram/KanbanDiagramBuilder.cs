using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.KanbanDiagram.Model;

using Task = FoggyBalrog.MermaidDotNet.KanbanDiagram.Model.Task;

namespace FoggyBalrog.MermaidDotNet.KanbanDiagram;

/// <summary>
/// A builder for kanban diagrams.
/// </summary>
public class KanbanDiagramBuilder
{
    private readonly MermaidConfig? _config;
    private readonly string? _title;
    private readonly bool _isSafe;
    private readonly List<Column> _columns = [];

    internal KanbanDiagramBuilder(string? title, MermaidConfig? config, bool isSafe)
    {
        if (isSafe)
        {
            title.ThrowIfWhiteSpace();
        }

        _title = title;
        _config = config;
        _isSafe = isSafe;
    }

    public KanbanDiagramBuilder AddColumn(string title, Action<KanbanDiagramColumnBuilder>? tasksAction = null)
    {
        if(_isSafe)
        {
            title.ThrowIfEmpty();
        }

        var columnBuilder = new KanbanDiagramColumnBuilder(title);

        if (tasksAction is not null)
        {
            tasksAction(columnBuilder);
        }

        _columns.Add(columnBuilder.ToColumn());

        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the kanban diagram.
    /// </summary>
    /// <returns>The Mermaid code for the kanban diagram.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("kanban");

        for (int i = 0; i < _columns.Count; i++)
        {
            var column = _columns[i];
            builder.AppendLine($"{Shared.Indent}column{i}[{column.Title}]");

            for (int j = 0; j < column.Tasks.Count; j++)
            {
                var task = column.Tasks[j];
                string metadata = task.HasMetadata ? BuildMetadataString(task) : string.Empty;
                builder.AppendLine($"{Shared.Indent}{Shared.Indent}task{i}{j}[{task.Description}]{metadata}");
            }
        }
        
        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private string BuildMetadataString(Task task)
    {
        var props = new List<string>();
        
        if(task.Assigned is not null)
        {
            props.Add($"assigned: '{task.Assigned}'");
        }

        if(task.Ticket is not null)
        {
            props.Add($"ticket: {task.Ticket}");
        }

        if(task.Priority is not null)
        {
            string priorityLabel = task.Priority switch
            {
                Priority.VeryHigh => "Very High",
                Priority.High => "High",
                Priority.Low => "Low",
                Priority.VeryLow => "Very Low",
                _ => throw MermaidException.InvalidOperation($"Unsupported priority {task.Priority}")
            };
            
            props.Add($"priority: '{priorityLabel}'");
        }

        return $"@{{ {string.Join(", ", props)} }}";
    }
}
