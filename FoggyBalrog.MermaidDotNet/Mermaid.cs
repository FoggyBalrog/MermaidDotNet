using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;
using FoggyBalrog.MermaidDotNet.Flowchart;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;
using FoggyBalrog.MermaidDotNet.SequenceDiagram;

namespace FoggyBalrog.MermaidDotNet;

public static class Mermaid
{
    public static SequenceDiagramBuilder SequenceDiagram => new();

    public static EntityRelationshipDiagramBuilder EntityRelationshipDiagram => new();

    public static FlowchartBuilder Flowchart(FlowchartOrientation orientation = FlowchartOrientation.TopToBottom) => new(orientation);
}
