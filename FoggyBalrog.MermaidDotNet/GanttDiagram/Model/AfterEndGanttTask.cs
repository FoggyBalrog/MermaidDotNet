namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

public record AfterEndGanttTask(string Id, string Name, GanttTask AfterTask, DateTimeOffset End, TaskTags Tags) : GanttTask(Id, Name, Tags);
