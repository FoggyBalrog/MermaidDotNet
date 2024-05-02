﻿namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

[Flags]
public enum TaskTags
{
    None = 0,
    Active = 1 << 0,
    Done = 1 << 1,
    Critical = 1 << 2,
    Milestone = 1 << 3
}
