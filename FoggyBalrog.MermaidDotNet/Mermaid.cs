using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;
using FoggyBalrog.MermaidDotNet.SequenceDiagram;

namespace FoggyBalrog.MermaidDotNet;

public static class Mermaid
{
    public static SequenceDiagramBuilder SequenceDiagram => new();

    public static EntityRelationshipDiagramBuilder EntityRelationshipDiagram => new();
}
