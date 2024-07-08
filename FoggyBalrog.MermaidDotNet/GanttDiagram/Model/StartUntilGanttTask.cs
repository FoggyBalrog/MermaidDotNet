namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

internal record StartUntilGanttTask(string Id, string Name, DateTimeOffset Start, GanttTask UntilTask, TaskTags Tags) : GanttTask(Id, Name, Tags);
