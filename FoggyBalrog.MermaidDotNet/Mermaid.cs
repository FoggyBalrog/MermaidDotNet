using FoggyBalrog.MermaidDotNet.ClassDiagram;
using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;
using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;
using FoggyBalrog.MermaidDotNet.Flowchart;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;
using FoggyBalrog.MermaidDotNet.GanttDiagram;
using FoggyBalrog.MermaidDotNet.GitGraph;
using FoggyBalrog.MermaidDotNet.MindMap;
using FoggyBalrog.MermaidDotNet.PieChart;
using FoggyBalrog.MermaidDotNet.QuadrantChart;
using FoggyBalrog.MermaidDotNet.RequirementDiagram;
using FoggyBalrog.MermaidDotNet.SequenceDiagram;
using FoggyBalrog.MermaidDotNet.StateDiagram;
using FoggyBalrog.MermaidDotNet.StateDiagram.Model;
using FoggyBalrog.MermaidDotNet.TimelineDiagram;
using FoggyBalrog.MermaidDotNet.UserJourneyDiagram;

namespace FoggyBalrog.MermaidDotNet;

/// <summary>
/// The main entry point for creating Mermaid diagrams. Exposes builders for all diagram types.
/// </summary>
public static class Mermaid
{
    /// <summary>
    /// Starts creating a class diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="direction">An optional direction for the diagram. If not specified, the default direction from Mermaid will be used on rendering.</param>
    /// <returns>A new <see cref="ClassDiagramBuilder"/> instance.</returns>
    public static ClassDiagramBuilder ClassDiagram(string? title = null, ClassDiagramDirection? direction = null) => new(title, direction);

    /// <summary>
    /// Starts creating an entity relationship diagram.
    /// </summary>
    /// <returns>A new <see cref="EntityRelationshipDiagramBuilder"/> instance.</returns>
    public static EntityRelationshipDiagramBuilder EntityRelationshipDiagram() => new();

    /// <summary>
    /// Starts creating a flowchart.
    /// </summary>
    /// <param name="orientation">An optional orientation for the flowchart. If not specified, the default orientation from Mermaid will be used on rendering.</param>
    /// <returns>A new <see cref="FlowchartBuilder"/> instance.</returns>
    public static FlowchartBuilder Flowchart(FlowchartOrientation orientation = FlowchartOrientation.TopToBottom) => new(orientation);

    /// <summary>
    /// Starts creating a Gantt diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="compactMode">Specifies whether the compact mode should be used.</param>
    /// <param name="hideTodayMarker">Specifies whether the today marker should be hidden.</param>
    /// <param name="dateFormat">Specifies the date format to use. Refer to the Mermaid documentation for supported formats.</param>
    /// <param name="axisFormat">An optional format for the axis. If not specified, the default format from Mermaid will be used on rendering. Refer to the Mermaid documentation for supported formats.</param>
    /// <param name="tickInterval">An optional tick interval for the diagram. If not specified, the default interval from Mermaid will be used on rendering. Refer to the Mermaid documentation for supported intervals.</param>
    /// <param name="weekIntervalStartDay">An optional start day for the week interval. If not specified, the default start day from Mermaid will be used on rendering. Refer to the Mermaid documentation for supported start days.</param>
    /// <returns>A new <see cref="GanttDiagramBuilder"/> instance.</returns>
    public static GanttDiagramBuilder GanttDiagram(
        string? title = null,
        bool compactMode = false,
        bool hideTodayMarker = false,
        string dateFormat = "YYYY-MM-DD",
        string? axisFormat = null,
        string? tickInterval = null,
        string? weekIntervalStartDay = null) => new(title, compactMode, hideTodayMarker, dateFormat, axisFormat, tickInterval, weekIntervalStartDay);

    /// <summary>
    /// Starts creating a Git graph.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="parallelCommits">Specifies whether to display commits in parallel.</param>
    /// <param name="vertical">Specifies whether to display the graph vertically.</param>
    /// <returns>A new <see cref="GitGraphBuilder"/> instance.</returns>
    public static GitGraphBuilder GitGraph(
        string? title = null,
        bool parallelCommits = false,
        bool vertical = false) => new(title, parallelCommits, vertical);

    /// <summary>
    /// Starts creating a mind map.
    /// </summary>
    /// <param name="rootText">The text for the root node.</param>
    /// <param name="rootShape">The shape for the root node. If not specified, the default shape from Mermaid will be used on rendering.</param>
    /// <returns>A new <see cref="MindMapBuilder"/> instance.</returns>
    public static MindMapBuilder MindMap(string rootText, MindMap.Model.NodeShape rootShape = MermaidDotNet.MindMap.Model.NodeShape.Default) => new(rootText, rootShape);

    /// <summary>
    /// Starts creating a pie chart.
    /// </summary>
    /// <param name="displayValuesOnLegend">Specifies whether to display values on the legend.</param>
    /// <param name="title">An optional title for the diagram.</param>
    /// <returns>A new <see cref="PieChartBuilder"/> instance.</returns>
    public static PieChartBuilder PieChart(bool displayValuesOnLegend = false, string? title = null) => new(displayValuesOnLegend, title);

    /// <summary>
    /// Starts creating a quadrant chart.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="quadrant1">An optional label for the first quadrant.</param>
    /// <param name="quadrant2">An optional label for the second quadrant.</param>
    /// <param name="quadrant3">An optional label for the third quadrant.</param>
    /// <param name="quadrant4">An optional label for the fourth quadrant.</param>
    /// <returns>A new <see cref="QuadrantChartBuilder"/> instance.</returns>
    public static QuadrantChartBuilder QuadrantChart(
        string? title = null,
        string? quadrant1 = null,
        string? quadrant2 = null,
        string? quadrant3 = null,
        string? quadrant4 = null) => new(title, quadrant1, quadrant2, quadrant3, quadrant4);

    /// <summary>
    /// Starts creating a requirement diagram.
    /// </summary>
    /// <returns>A new <see cref="RequirementDiagramBuilder"/> instance.</returns>
    public static RequirementDiagramBuilder RequirementDiagram() => new();

    /// <summary>
    /// Starts creating a sequence diagram.
    /// </summary>
    /// <param name="autonumber">Specifies whether to automatically number the sequence items.</param>
    /// <returns>A new <see cref="SequenceDiagramBuilder"/> instance.</returns>
    public static SequenceDiagramBuilder SequenceDiagram(bool autonumber = false) => new(autonumber);

    /// <summary>
    /// Starts creating a state diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="direction">An optional direction for the diagram. If not specified, the default direction from Mermaid will be used on rendering.</param>
    /// <returns>A new <see cref="StateDiagramBuilder"/> instance.</returns>
    public static StateDiagramBuilder StateDiagram(string? title = null, StateDiagramDirection? direction = null) => new(title, direction);

    /// <summary>
    /// Starts creating a timeline diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <returns>A new <see cref="TimelineDiagramBuilder"/> instance.</returns>
    public static TimelineDiagramBuilder TimelineDiagram(string? title = null) => new(title);

    /// <summary>
    /// Starts creating a user journey diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <returns>A new <see cref="UserJourneyDiagramBuilder"/> instance.</returns>
    public static UserJourneyDiagramBuilder UserJourneyDiagram(string? title = null) => new(title);
}
