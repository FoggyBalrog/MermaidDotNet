using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;
using FoggyBalrog.MermaidDotNet.Flowchart;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;
using FoggyBalrog.MermaidDotNet.SequenceDiagram;

namespace FoggyBalrog.MermaidDotNet;

public static class Mermaid
{
    public static EntityRelationshipDiagramBuilder EntityRelationshipDiagram() => new();
    public static FlowchartBuilder Flowchart(FlowchartOrientation orientation = FlowchartOrientation.TopToBottom) => new(orientation);
    public static SequenceDiagramBuilder SequenceDiagram(bool autonumber = false) => new(autonumber);
}
