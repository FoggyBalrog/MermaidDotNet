namespace FoggyBalrog.MermaidDotNet.UnitTests.KanbanDiagram;

public class KanbanDiagramSafeModeValidationTests
{
    [Fact]
    public void KanbanDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.KanbanDiagram(title: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}