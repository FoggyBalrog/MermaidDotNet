namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

/// <summary>
/// Tags for tasks.
/// </summary>
[Flags]
public enum TaskTags
{
    None = 0,
    Active = 1 << 0,
    Done = 1 << 1,
    Critical = 1 << 2,
    Milestone = 1 << 3
}
