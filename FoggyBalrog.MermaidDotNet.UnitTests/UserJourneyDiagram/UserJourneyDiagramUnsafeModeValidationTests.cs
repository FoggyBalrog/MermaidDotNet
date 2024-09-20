namespace FoggyBalrog.MermaidDotNet.UnitTests.UserJourneyDiagram;

public class UserJourneyDiagramUnsafeModeValidationTests
{
    [Fact]
    public void UserJourneyDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .UserJourneyDiagram(" ")
            .Build();
    }

    [Fact]
    public void AddTask_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .UserJourneyDiagram("Title")
            .AddTask(" ", 1, "Actor 1", "Actor 2", "Actor 3")
            .Build();
    }

    [Fact]
    public void AddTask_DoesNotThrowIfAnyActorIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .UserJourneyDiagram("Title")
            .AddTask("Description", 1, "Actor 1", " ", "Actor 3")
            .Build();
    }

    [Fact]
    public void AddSection_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .UserJourneyDiagram("Title")
            .AddSection(" ")
            .Build();
    }
}
