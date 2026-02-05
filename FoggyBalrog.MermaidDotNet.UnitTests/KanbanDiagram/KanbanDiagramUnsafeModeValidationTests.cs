namespace FoggyBalrog.MermaidDotNet.UnitTests.KanbanDiagram;

public class KanbanDiagramUnsafeModeValidationTests
{
    [Fact]
    public void KanbanDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .KanbanDiagram(title: " ");
    }
}
