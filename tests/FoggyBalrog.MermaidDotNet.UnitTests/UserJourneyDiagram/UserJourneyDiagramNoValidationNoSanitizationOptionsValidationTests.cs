namespace FoggyBalrog.MermaidDotNet.UnitTests.UserJourneyDiagram;

public class UserJourneyDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void UserJourneyDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .UserJourneyDiagram(" ", options: _options)
            .Build();
    }

    [Fact]
    public void AddTask_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .UserJourneyDiagram("Title", options: _options)
            .AddTask(" ", 1, "Actor 1", "Actor 2", "Actor 3")
            .Build();
    }

    [Fact]
    public void AddTask_DoesNotThrowIfAnyActorIsWhiteSpace()
    {
        Mermaid
            .UserJourneyDiagram("Title", options: _options)
            .AddTask("Description", 1, "Actor 1", " ", "Actor 3")
            .Build();
    }

    [Fact]
    public void AddSection_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .UserJourneyDiagram("Title", options: _options)
            .AddSection(" ")
            .Build();
    }
}
