namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

/// <summary>
/// Represents a task in a Gantt diagram.
/// </summary>
public abstract record GanttTask : IGanttItem
{
    protected GanttTask(string id, string name, TaskTags tags)
    {
        Id = id;
        Name = name;
        Tags = tags;
    }

    /// <summary>
    /// The unique identifier of the task.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The name of the task.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The tags of the task.
    /// </summary>
    public TaskTags Tags { get; }

    internal ITaskClickBinding? ClickBinding { get; set; } = null;
}
