using System.Collections.ObjectModel;

namespace FoggyBalrog.MermaidDotNet;

internal sealed record MermaidFeatureInfo(
    string Name,
    MermaidVersion MinimumVersion,
    MermaidVersion? RemovedInVersion = null,
    string? MigrationGuidance = null);

internal static class MermaidFeatureRegistry
{
    internal static ReadOnlyDictionary<MermaidFeature, MermaidFeatureInfo> All { get; } = new(new Dictionary<MermaidFeature, MermaidFeatureInfo>
    {
        [MermaidFeature.Flowchart] = new("Flowchart", MermaidVersion.V11_0),
        [MermaidFeature.SequenceDiagram] = new("Sequence diagram", MermaidVersion.V11_0),
        [MermaidFeature.ClassDiagram] = new("Class diagram", MermaidVersion.V11_0),
        [MermaidFeature.StateDiagram] = new("State diagram", MermaidVersion.V11_0),
        [MermaidFeature.EntityRelationshipDiagram] = new("Entity relationship diagram", MermaidVersion.V11_0),
        [MermaidFeature.UserJourneyDiagram] = new("User journey diagram", MermaidVersion.V11_0),
        [MermaidFeature.GanttDiagram] = new("Gantt diagram", MermaidVersion.V11_0),
        [MermaidFeature.GitGraph] = new("Git graph", MermaidVersion.V11_0),
        [MermaidFeature.MindMap] = new("Mind map", MermaidVersion.V11_0),
        [MermaidFeature.KanbanDiagram] = new("Kanban diagram", MermaidVersion.V11_4),
        [MermaidFeature.PacketDiagram] = new("Packet diagram (packet header)", MermaidVersion.V11_9),
        [MermaidFeature.PieChart] = new("Pie chart", MermaidVersion.V11_0),
        [MermaidFeature.QuadrantChart] = new("Quadrant chart", MermaidVersion.V11_0),
        [MermaidFeature.RequirementDiagram] = new("Requirement diagram", MermaidVersion.V11_0),
        [MermaidFeature.TimelineDiagram] = new("Timeline diagram", MermaidVersion.V11_0),
        [MermaidFeature.SankeyDiagram] = new("Sankey diagram (sankey header)", MermaidVersion.V11_10),
        [MermaidFeature.XYChart] = new("XY chart (xychart header)", MermaidVersion.V11_10),
        [MermaidFeature.BlockDiagram] = new("Block diagram (block header)", MermaidVersion.V11_10),
        [MermaidFeature.FlowchartExpandedNodeShapes] = new("Flowchart expanded node shapes", MermaidVersion.V11_3, MigrationGuidance: "Use AddNode with a legacy NodeShape for older targets."),
        [MermaidFeature.FlowchartEdgeCurves] = new("Flowchart per-edge curves", MermaidVersion.V11_10, MigrationGuidance: "Omit curveStyle or use the diagram-wide Flowchart.Curve configuration for older targets."),
        [MermaidFeature.GanttVerticalMarkers] = new("Gantt vertical markers", MermaidVersion.V11_7),
        [MermaidFeature.PacketBitsSyntax] = new("Packet bits syntax", MermaidVersion.V11_7, MigrationGuidance: "Use AddFieldWithEnd for absolute field ranges on older targets."),
        [MermaidFeature.StateHyperlinks] = new("State hyperlinks", MermaidVersion.V11_7),
        [MermaidFeature.SequenceParticipantShapesWithAliases] = new("Sequence participant shapes with aliases", MermaidVersion.V11_13, MigrationGuidance: "Use MemberType.Participant or MemberType.Actor for older targets."),
        [MermaidFeature.ClassDottedNamespaces] = new("Class dotted namespaces", MermaidVersion.V11_3),
        [MermaidFeature.ClassNestedNamespaces] = new("Class nested namespaces", MermaidVersion.V11_15, MigrationGuidance: "Use flat namespaces for older targets."),
        [MermaidFeature.ERMultilineRelationshipLabels] = new("ER multiline relationship labels", MermaidVersion.V11_1, MigrationGuidance: "Use a single-line relationship label for older targets."),
        [MermaidFeature.ElkCycleBreakingStrategy] = new("ELK cycleBreakingStrategy configuration", MermaidVersion.V11_1),
        [MermaidFeature.KanbanConfiguration] = new("Kanban configuration", MermaidVersion.V11_4),
        [MermaidFeature.UserJourneyTitleStyling] = new("User journey title styling configuration", MermaidVersion.V11_7),
        [MermaidFeature.XYChartDataLabels] = new("XY chart showDataLabel configuration", MermaidVersion.V11_7),
        [MermaidFeature.FlowchartDefaultRenderer] = new("Flowchart defaultRenderer configuration", MermaidVersion.V11_0, MermaidVersion.V12_0, "Remove Flowchart.DefaultRenderer and set top-level MermaidConfig.Layout to \"dagre\" or \"elk\" instead."),
        [MermaidFeature.ClassDefaultRenderer] = new("Class defaultRenderer configuration", MermaidVersion.V11_0, MermaidVersion.V12_0, "Remove Class.DefaultRenderer and set top-level MermaidConfig.Layout to \"dagre\" or \"elk\" instead."),
        [MermaidFeature.StateDefaultRenderer] = new("State defaultRenderer configuration", MermaidVersion.V11_0, MermaidVersion.V12_0, "Remove State.DefaultRenderer and set top-level MermaidConfig.Layout to \"dagre\" or \"elk\" instead.")
    });
}
