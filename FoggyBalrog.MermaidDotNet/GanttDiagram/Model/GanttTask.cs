namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

public abstract record GanttTask(string Id, string Name, TaskTags Tags) : IGanttItem
{
    internal ITaskClickBinding? ClickBinding { get; set; } = null;
}
