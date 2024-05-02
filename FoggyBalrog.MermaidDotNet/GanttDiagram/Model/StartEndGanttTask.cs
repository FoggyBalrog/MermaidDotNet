namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

public record StartEndGanttTask(string Id, string Name, DateTimeOffset Start, DateTimeOffset End, TaskTags Tags) : GanttTask(Id, Name, Tags);
