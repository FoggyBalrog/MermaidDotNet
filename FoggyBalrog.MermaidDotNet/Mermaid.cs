using FoggyBalrog.MermaidDotNet.ClassDiagram;
using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;
using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;
using FoggyBalrog.MermaidDotNet.Flowchart;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;
using FoggyBalrog.MermaidDotNet.GanttDiagram;
using FoggyBalrog.MermaidDotNet.MindMap;
using FoggyBalrog.MermaidDotNet.PieChart;
using FoggyBalrog.MermaidDotNet.QuadrantChart;
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
    public static GanttDiagramBuilder GanttDiagram(
        string? title = null,
        bool compactMode = false,
        bool hideTodayMarker = false,
        string dateFormat = "YYYY-MM-DD",
        string? axisFormat = null,
        string? tickInterval = null,
        string? weekIntervalStartDay = null) => new(title, compactMode, hideTodayMarker, dateFormat, axisFormat, tickInterval, weekIntervalStartDay);
    public static MindMapBuilder MindMap(string rootText, MindMap.Model.NodeShape rootShape = MermaidDotNet.MindMap.Model.NodeShape.Default) => new(rootText, rootShape);
    public static PieChartBuilder PieChart(bool displayValuesOnLegend = false, string? title = null) => new(displayValuesOnLegend, title);
    public static QuadrantChartBuilder QuadrantChart(
        string? title = null,
        string? quadrant1 = null,
        string? quadrant2 = null,
        string? quadrant3 = null,
        string? quadrant4 = null) => new(title, quadrant1, quadrant2, quadrant3, quadrant4);
    public static SequenceDiagramBuilder SequenceDiagram(bool autonumber = false) => new(autonumber);
    public static StateDiagramBuilder StateDiagram(string? title = null, StateDiagramDirection? direction = null) => new(title, direction);
    public static TimelineDiagramBuilder TimelineDiagram(string? title = null) => new(title);
    public static UserJourneyDiagramBuilder UserJourneyDiagram(string? title = null) => new(title);
}
