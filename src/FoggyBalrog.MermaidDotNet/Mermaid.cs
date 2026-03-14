using FoggyBalrog.MermaidDotNet.BlockDiagram;
using FoggyBalrog.MermaidDotNet.ClassDiagram;
using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;
using FoggyBalrog.MermaidDotNet.Flowchart;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;
using FoggyBalrog.MermaidDotNet.GanttDiagram;
using FoggyBalrog.MermaidDotNet.GitGraph;
using FoggyBalrog.MermaidDotNet.KanbanDiagram;
using FoggyBalrog.MermaidDotNet.MindMap;
using FoggyBalrog.MermaidDotNet.PacketDiagram;
using FoggyBalrog.MermaidDotNet.PieChart;
using FoggyBalrog.MermaidDotNet.QuadrantChart;
using FoggyBalrog.MermaidDotNet.RequirementDiagram;
using FoggyBalrog.MermaidDotNet.SankeyDiagram;
using FoggyBalrog.MermaidDotNet.SequenceDiagram;
using FoggyBalrog.MermaidDotNet.StateDiagram;
using FoggyBalrog.MermaidDotNet.StateDiagram.Model;
using FoggyBalrog.MermaidDotNet.TimelineDiagram;
using FoggyBalrog.MermaidDotNet.UserJourneyDiagram;
using FoggyBalrog.MermaidDotNet.XYChart;
using FoggyBalrog.MermaidDotNet.XYChart.Model;

namespace FoggyBalrog.MermaidDotNet;

/// <summary>
/// The main entry point for creating Mermaid diagrams. Exposes builders for all diagram types.
/// </summary>
public static class Mermaid
{
    public static BlockDiagramBuilder BlockDiagram(
        string? title = null,
        MermaidConfig? config = null,
        int? columns = null,
        MermaidDotNetOptions? options = null) => new(title, config, options, columns);

    public static ClassDiagramBuilder ClassDiagram(
        string? title = null,
        MermaidConfig? config = null,
        ClassDiagramDirection? direction = null,
        MermaidDotNetOptions? options = null) => new(title, config, direction, options);

    public static EntityRelationshipDiagramBuilder EntityRelationshipDiagram(
        string? title = null,
        MermaidConfig? config = null,
        MermaidDotNetOptions? options = null) => new(title, config, options);

    public static FlowchartBuilder Flowchart(
        string? title = null,
        MermaidConfig? config = null,
        FlowchartOrientation orientation = FlowchartOrientation.TopToBottom,
        MermaidDotNetOptions? options = null) => new(title, config, orientation, options);

    public static GanttDiagramBuilder GanttDiagram(
        string? title = null,
        MermaidConfig? config = null,
        bool hideTodayMarker = false,
        string? todayMarkerCss = null,
        string dateFormat = "YYYY-MM-DD",
        MermaidDotNetOptions? options = null) => new(title, config, hideTodayMarker, todayMarkerCss, dateFormat, options);

    public static KanbanDiagramBuilder KanbanDiagram(
        string? title = null,
        MermaidConfig? config = null,
        MermaidDotNetOptions? options = null) => new(title, config, options);

    public static GitGraphBuilder GitGraph(
        string? title = null,
        MermaidConfig? config = null,
        bool vertical = false,
        MermaidDotNetOptions? options = null) => new(title, config, vertical, options);

    public static MindMapBuilder MindMap(
        string rootText,
        string? title = null,
        MermaidConfig? config = null,
        MindMap.Model.NodeShape rootShape = MermaidDotNet.MindMap.Model.NodeShape.Default,
        bool rootIsMarkdown = false,
        string? rootIcon = null,
        MermaidDotNetOptions? options = null,
        params string[] rootClasses) => new(rootText, title, config, rootShape, rootIsMarkdown, rootIcon, rootClasses, options);

    public static PacketDiagramBuilder PacketDiagram(
        string? title = null,
        MermaidConfig? config = null,
        MermaidDotNetOptions? options = null) => new(title, config, options);

    public static PieChartBuilder PieChart(
        string? title = null,
        MermaidConfig? config = null,
        bool displayValuesOnLegend = false,
        MermaidDotNetOptions? options = null) => new(title, config, displayValuesOnLegend, options);

    public static QuadrantChartBuilder QuadrantChart(
        string? title = null,
        MermaidConfig? config = null,
        string? quadrant1 = null,
        string? quadrant2 = null,
        string? quadrant3 = null,
        string? quadrant4 = null,
        MermaidDotNetOptions? options = null) => new(title, config, quadrant1, quadrant2, quadrant3, quadrant4, options);

    public static RequirementDiagramBuilder RequirementDiagram(
        string? title = null,
        MermaidConfig? config = null,
        MermaidDotNetOptions? options = null) => new(title, config, options);

    public static SankeyDiagramBuilder SankeyDiagram(
        string? title = null,
        MermaidConfig? config = null,
        MermaidDotNetOptions? options = null) => new(title, config, options);

    public static SequenceDiagramBuilder SequenceDiagram(
        string? title = null,
        MermaidConfig? config = null,
        bool autonumber = false,
        MermaidDotNetOptions? options = null) => new(title, config, autonumber, options);

    public static StateDiagramBuilder StateDiagram(
        string? title = null,
        MermaidConfig? config = null,
        StateDiagramDirection? direction = null,
        MermaidDotNetOptions? options = null) => new(title, config, direction, options);

    public static TimelineDiagramBuilder TimelineDiagram(
        string? title = null,
        MermaidConfig? config = null,
        MermaidDotNetOptions? options = null) => new(title, config, options);

    public static UserJourneyDiagramBuilder UserJourneyDiagram(
        string? title = null,
        MermaidConfig? config = null,
        MermaidDotNetOptions? options = null) => new(title, config, options);

    public static XYChartBuilder XYChart(
        string? title = null,
        MermaidConfig? config = null,
        XYChartOrientation? orientation = null,
        MermaidDotNetOptions? options = null) => new(title, config, orientation, options);
}
