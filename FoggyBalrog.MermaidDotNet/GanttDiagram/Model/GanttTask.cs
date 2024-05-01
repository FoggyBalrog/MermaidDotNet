namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

public abstract record GanttTask(string Id, string Name, TaskTags Tags) : IGanttItem
{
    internal ITaskClickBindind? ClickBindind { get; set; } = null;
}
