namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

internal record AfterDurationGanttTask(string Id, string Name, GanttTask AfterTask, TimeSpan Duration, TaskTags Tags) : GanttTask(Id, Name, Tags);
