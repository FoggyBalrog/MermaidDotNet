﻿using FoggyBalrog.MermaidDotNet.ClassDiagram;
using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;
using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;
using FoggyBalrog.MermaidDotNet.Flowchart;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;
using FoggyBalrog.MermaidDotNet.PieChart;
using FoggyBalrog.MermaidDotNet.SequenceDiagram;
using FoggyBalrog.MermaidDotNet.StateDiagram;
using FoggyBalrog.MermaidDotNet.StateDiagram.Model;
using FoggyBalrog.MermaidDotNet.TimelineDiagram;
using FoggyBalrog.MermaidDotNet.UserJourneyDiagram;

namespace FoggyBalrog.MermaidDotNet;

public static class Mermaid
{
    public static ClassDiagramBuilder ClassDiagram(string? title = null, ClassDiagramDirection? direction = null) => new(title, direction);
    public static EntityRelationshipDiagramBuilder EntityRelationshipDiagram() => new();
    public static FlowchartBuilder Flowchart(FlowchartOrientation orientation = FlowchartOrientation.TopToBottom) => new(orientation);
    public static PieChartBuilder PieChart(bool displayValuesOnLegend = false, string? title = null) => new(displayValuesOnLegend, title);
    public static SequenceDiagramBuilder SequenceDiagram(bool autonumber = false) => new(autonumber);
    public static StateDiagramBuilder StateDiagram(string? title = null, StateDiagramDirection? direction = null) => new(title, direction);
    public static TimelineDiagramBuilder TimelineDiagram(string? title = null) => new(title);
    public static UserJourneyDiagramBuilder UserJourneyDiagram(string? title = null) => new(title);
}
