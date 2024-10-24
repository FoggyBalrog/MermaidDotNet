using FoggyBalrog.MermaidDotNet.ClassDiagram;
using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
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
    /// Disables the safe mode for creating diagrams. In unsafe mode, no validation is performed on the diagram data.
    /// </summary>
    public static class Unsafe
    {
        /// <summary>
        /// Starts creating a class diagram, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="direction">An optional direction for the diagram. If not specified, the default direction from Mermaid will be used on rendering.</param>
        /// <returns>A new <see cref="ClassDiagramBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static ClassDiagramBuilder ClassDiagram(
            string? title = null,
            MermaidConfig? config = null,
            ClassDiagramDirection? direction = null) => new(title, config, direction, isSafe: false);

        /// <summary>
        /// Starts creating an entity relationship diagram, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <returns>A new <see cref="EntityRelationshipDiagramBuilder"/> instance.</returns>
        public static EntityRelationshipDiagramBuilder EntityRelationshipDiagram(
            string? title = null,
            MermaidConfig? config = null) => new(title, config, isSafe: false);

        /// <summary>
        /// Starts creating a flowchart, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="orientation">An optional orientation for the flowchart. If not specified, the default orientation from Mermaid will be used on rendering.</param>
        /// <returns>A new <see cref="FlowchartBuilder"/> instance.</returns>
        public static FlowchartBuilder Flowchart(
            string? title = null,
            MermaidConfig? config = null,
            FlowchartOrientation orientation = FlowchartOrientation.TopToBottom) => new(title, config, orientation, isSafe: false);

        /// <summary>
        /// Starts creating a Gantt diagram, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="hideTodayMarker">Specifies whether the today marker should be hidden.</param>
        /// <param name="todayMarkerCss">An optional CSS class for the today marker. If not specified, the default class from Mermaid will be used on rendering.</param>
        /// <param name="dateFormat">Specifies the date format to use. Refer to the Mermaid documentation for supported formats.</param>
        /// <returns>A new <see cref="GanttDiagramBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        /// <exception cref="MermaidException">Thrown when <paramref name="dateFormat"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static GanttDiagramBuilder GanttDiagram(
            string? title = null,
            MermaidConfig? config = null,
            bool hideTodayMarker = false,
            string? todayMarkerCss = null,
            string dateFormat = "YYYY-MM-DD") => new(title, config, hideTodayMarker, todayMarkerCss, dateFormat, isSafe: false);

        /// <summary>
        /// Starts creating a Git graph, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="vertical">Specifies whether to display the graph vertically.</param>
        /// <returns>A new <see cref="GitGraphBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static GitGraphBuilder GitGraph(
            string? title = null,
            MermaidConfig? config = null,
            bool vertical = false) => new(title, config, vertical, isSafe: false);

        /// <summary>
        /// Starts creating a mind map, in unsafe mode.
        /// </summary>
        /// <param name="rootText">The text for the root node.</param>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="rootShape">The shape for the root node. If not specified, the default shape from Mermaid will be used on rendering.</param>
        /// <returns>A new <see cref="MindMapBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="rootText"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static MindMapBuilder MindMap(
            string rootText,
            string? title = null,
            MermaidConfig? config = null,
            MindMap.Model.NodeShape rootShape = MermaidDotNet.MindMap.Model.NodeShape.Default) => new(rootText, title, config, rootShape, isSafe: false);

        /// <summary>
        /// Starts creating a pie chart, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="displayValuesOnLegend">Specifies whether to display values on the legend.</param>
        /// <returns>A new <see cref="PieChartBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static PieChartBuilder PieChart(
            string? title = null,
            MermaidConfig? config = null,
            bool displayValuesOnLegend = false) => new(title, config, displayValuesOnLegend, isSafe: false);

        /// <summary>
        /// Starts creating a quadrant chart, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="quadrant1">An optional label for the first quadrant.</param>
        /// <param name="quadrant2">An optional label for the second quadrant.</param>
        /// <param name="quadrant3">An optional label for the third quadrant.</param>
        /// <param name="quadrant4">An optional label for the fourth quadrant.</param>
        /// <returns>A new <see cref="QuadrantChartBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="title"/>, <paramref name="quadrant1"/>, <paramref name="quadrant2"/>, <paramref name="quadrant3"/>, or <paramref name="quadrant4"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static QuadrantChartBuilder QuadrantChart(
            string? title = null,
            MermaidConfig? config = null,
            string? quadrant1 = null,
            string? quadrant2 = null,
            string? quadrant3 = null,
            string? quadrant4 = null) => new(title, config, quadrant1, quadrant2, quadrant3, quadrant4, isSafe: false);

        /// <summary>
        /// Starts creating a requirement diagram, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <returns>A new <see cref="RequirementDiagramBuilder"/> instance.</returns>
        public static RequirementDiagramBuilder RequirementDiagram(
            string? title = null,
            MermaidConfig? config = null) => new(title, config, isSafe: false);

        /// <summary>
        /// Starts creating a sequence diagram, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="autonumber">Specifies whether to automatically number the sequence items.</param>
        /// <returns>A new <see cref="SequenceDiagramBuilder"/> instance.</returns>
        public static SequenceDiagramBuilder SequenceDiagram(
            string? title = null,
            MermaidConfig? config = null,
            bool autonumber = false) => new(title, config, autonumber, isSafe: false);

        /// <summary>
        /// Starts creating a state diagram, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <param name="direction">An optional direction for the diagram. If not specified, the default direction from Mermaid will be used on rendering.</param>
        /// <returns>A new <see cref="StateDiagramBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static StateDiagramBuilder StateDiagram(
            string? title = null,
            MermaidConfig? config = null,
            StateDiagramDirection? direction = null) => new(title, config, direction, isSafe: false);

        /// <summary>
        /// Starts creating a timeline diagram, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <returns>A new <see cref="TimelineDiagramBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static TimelineDiagramBuilder TimelineDiagram(
            string? title = null,
            MermaidConfig? config = null) => new(title, config, isSafe: false);

        /// <summary>
        /// Starts creating a user journey diagram, in unsafe mode.
        /// </summary>
        /// <param name="title">An optional title for the diagram.</param>
        /// <param name="config">An optional configuration for the diagram.</param>
        /// <returns>A new <see cref="UserJourneyDiagramBuilder"/> instance.</returns>
        /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
        public static UserJourneyDiagramBuilder UserJourneyDiagram(
            string? title = null,
            MermaidConfig? config = null) => new(title, config, isSafe: false);
    }

    /// <summary>
    /// Starts creating a class diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="direction">An optional direction for the diagram. If not specified, the default direction from Mermaid will be used on rendering.</param>
    /// <returns>A new <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static ClassDiagramBuilder ClassDiagram(
        string? title = null,
        MermaidConfig? config = null,
        ClassDiagramDirection? direction = null) => new(title, config, direction, isSafe: true);

    /// <summary>
    /// Starts creating an entity relationship diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <returns>A new <see cref="EntityRelationshipDiagramBuilder"/> instance.</returns>
    public static EntityRelationshipDiagramBuilder EntityRelationshipDiagram(
        string? title = null,
        MermaidConfig? config = null) => new(title, config, isSafe: true);

    /// <summary>
    /// Starts creating a flowchart.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="orientation">An optional orientation for the flowchart. If not specified, the default orientation from Mermaid will be used on rendering.</param>
    /// <returns>A new <see cref="FlowchartBuilder"/> instance.</returns>
    public static FlowchartBuilder Flowchart(
        string? title = null,
        MermaidConfig? config = null,
        FlowchartOrientation orientation = FlowchartOrientation.TopToBottom) => new(title, config, orientation, isSafe: true);

    /// <summary>
    /// Starts creating a Gantt diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="hideTodayMarker">Specifies whether the today marker should be hidden.</param>
    /// <param name="todayMarkerCss">An optional CSS class for the today marker. If not specified, the default class from Mermaid will be used on rendering.</param>
    /// <param name="dateFormat">Specifies the date format to use. Refer to the Mermaid documentation for supported formats.</param>
    /// <returns>A new <see cref="GanttDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="dateFormat"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static GanttDiagramBuilder GanttDiagram(
        string? title = null,
        MermaidConfig? config = null,
        bool hideTodayMarker = false,
        string? todayMarkerCss = null,
        string dateFormat = "YYYY-MM-DD") => new(title, config, hideTodayMarker, todayMarkerCss, dateFormat, isSafe: true);

    /// <summary>
    /// Starts creating a Git graph.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="vertical">Specifies whether to display the graph vertically.</param>
    /// <returns>A new <see cref="GitGraphBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static GitGraphBuilder GitGraph(
        string? title = null,
        MermaidConfig? config = null,
        bool vertical = false) => new(title, config, vertical, isSafe: true);

    /// <summary>
    /// Starts creating a mind map.
    /// </summary>
    /// <param name="rootText">The text for the root node.</param>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="rootShape">The shape for the root node. If not specified, the default shape from Mermaid will be used on rendering.</param>
    /// <returns>A new <see cref="MindMapBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="rootText"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static MindMapBuilder MindMap(
        string rootText,
        string? title = null,
        MermaidConfig? config = null,
        MindMap.Model.NodeShape rootShape = MermaidDotNet.MindMap.Model.NodeShape.Default) => new(rootText, title, config, rootShape, isSafe: true);

    /// <summary>
    /// Starts creating a pie chart.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="displayValuesOnLegend">Specifies whether to display values on the legend.</param>
    /// <returns>A new <see cref="PieChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static PieChartBuilder PieChart(
        string? title = null,
        MermaidConfig? config = null,
        bool displayValuesOnLegend = false) => new(title, config, displayValuesOnLegend, isSafe: true);

    /// <summary>
    /// Starts creating a quadrant chart.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="quadrant1">An optional label for the first quadrant.</param>
    /// <param name="quadrant2">An optional label for the second quadrant.</param>
    /// <param name="quadrant3">An optional label for the third quadrant.</param>
    /// <param name="quadrant4">An optional label for the fourth quadrant.</param>
    /// <returns>A new <see cref="QuadrantChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/>, <paramref name="quadrant1"/>, <paramref name="quadrant2"/>, <paramref name="quadrant3"/>, or <paramref name="quadrant4"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static QuadrantChartBuilder QuadrantChart(
        string? title = null,
        MermaidConfig? config = null,
        string? quadrant1 = null,
        string? quadrant2 = null,
        string? quadrant3 = null,
        string? quadrant4 = null) => new(title, config, quadrant1, quadrant2, quadrant3, quadrant4, isSafe: true);

    /// <summary>
    /// Starts creating a requirement diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <returns>A new <see cref="RequirementDiagramBuilder"/> instance.</returns>
    public static RequirementDiagramBuilder RequirementDiagram(
        string? title = null,
        MermaidConfig? config = null) => new(title, config, isSafe: true);

    /// <summary>
    /// Starts creating a sequence diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="autonumber">Specifies whether to automatically number the sequence items.</param>
    /// <returns>A new <see cref="SequenceDiagramBuilder"/> instance.</returns>
    public static SequenceDiagramBuilder SequenceDiagram(
        string? title = null,
        MermaidConfig? config = null,
        bool autonumber = false) => new(title, config, autonumber, isSafe: true);

    /// <summary>
    /// Starts creating a state diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <param name="direction">An optional direction for the diagram. If not specified, the default direction from Mermaid will be used on rendering.</param>
    /// <returns>A new <see cref="StateDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static StateDiagramBuilder StateDiagram(
        string? title = null,
        MermaidConfig? config = null,
        StateDiagramDirection? direction = null) => new(title, config, direction, isSafe: true);

    /// <summary>
    /// Starts creating a timeline diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <returns>A new <see cref="TimelineDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static TimelineDiagramBuilder TimelineDiagram(
        string? title = null,
        MermaidConfig? config = null) => new(title, config, isSafe: true);

    /// <summary>
    /// Starts creating a user journey diagram.
    /// </summary>
    /// <param name="title">An optional title for the diagram.</param>
    /// <param name="config">An optional configuration for the diagram.</param>
    /// <returns>A new <see cref="UserJourneyDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public static UserJourneyDiagramBuilder UserJourneyDiagram(
        string? title = null,
        MermaidConfig? config = null) => new(title, config, isSafe: true);
}
