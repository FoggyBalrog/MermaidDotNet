namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

public record AfterUntilGanttTask(string Id, string Name, GanttTask AfterTask, GanttTask UntilTask, TaskTags Tags) : GanttTask(Id, Name, Tags);
