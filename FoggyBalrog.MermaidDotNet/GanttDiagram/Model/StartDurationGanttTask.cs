namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

public record StartDurationGanttTask(string Id, string Name, DateTimeOffset Start, TimeSpan Duration, TaskTags Tags) : GanttTask(Id, Name, Tags);
