namespace FoggyBalrog.MermaidDotNet.UnitTests.UserJourneyDiagram;

public class UserJourneyDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .UserJourneyDiagram(options: _options)
            .AddTask("Task:", 1, "Actor,", "Actor:")
            .AddSection("Section:")
            .Build();

        Assert.Contains("Task:: 1: Actor,, Actor:", diagram);
        Assert.Contains("section Section:", diagram);
        Assert.DoesNotContain("#58;", diagram);
    }
}
