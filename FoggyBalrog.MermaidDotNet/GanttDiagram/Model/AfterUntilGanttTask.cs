namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

internal record AfterUntilGanttTask(string Id, string Name, GanttTask AfterTask, GanttTask UntilTask, TaskTags Tags) : GanttTask(Id, Name, Tags);
