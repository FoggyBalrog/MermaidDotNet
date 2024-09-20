namespace FoggyBalrog.MermaidDotNet.UnitTests.TimelineDiagram;

public class TimelineDiagramUnsafeModeValidationTests
{
    [Fact]
    public void TimelineDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .TimelineDiagram(" ")
            .Build();
    }

    [Fact]
    public void AddEvents_DoesNotThrowIfTimePeriodIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .TimelineDiagram()
            .AddEvents(" ", "Event 1", "Event 2", "Event 3")
            .Build();
    }

    [Fact]
    public void AddEvents_DoesNotThrowIfAnyEventIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .TimelineDiagram()
            .AddEvents("Time Period", "Event 1", " ", "Event 3")
            .Build();
    }

    [Fact]
    public void AddSection_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .TimelineDiagram()
            .AddSection(" ")
            .Build();
    }
}
