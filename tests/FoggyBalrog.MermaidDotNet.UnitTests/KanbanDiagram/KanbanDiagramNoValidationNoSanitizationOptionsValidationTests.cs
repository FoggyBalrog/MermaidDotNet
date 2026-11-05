namespace FoggyBalrog.MermaidDotNet.UnitTests.KanbanDiagram;

public class KanbanDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void KanbanDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .KanbanDiagram(title: " ", options: _options);
    }
}
