namespace FoggyBalrog.MermaidDotNet.UnitTests.TimelineDiagram;

public class TimelineDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void TimelineDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .TimelineDiagram(" ", options: _options)
            .Build();
    }

    [Fact]
    public void AddEvents_DoesNotThrowIfTimePeriodIsWhiteSpace()
    {
        Mermaid
            .TimelineDiagram(options: _options)
            .AddEvents(" ", "Event 1", "Event 2", "Event 3")
            .Build();
    }

    [Fact]
    public void AddEvents_DoesNotThrowIfAnyEventIsWhiteSpace()
    {
        Mermaid
            .TimelineDiagram(options: _options)
            .AddEvents("Time Period", "Event 1", " ", "Event 3")
            .Build();
    }

    [Fact]
    public void AddSection_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .TimelineDiagram(options: _options)
            .AddSection(" ")
            .Build();
    }
}
