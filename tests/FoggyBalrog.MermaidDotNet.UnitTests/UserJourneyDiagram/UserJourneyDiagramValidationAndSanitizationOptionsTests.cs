namespace FoggyBalrog.MermaidDotNet.UnitTests.UserJourneyDiagram;

public class UserJourneyDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .UserJourneyDiagram(options: _options)
            .AddTask("Task:", 1, "Actor,", "Actor:")
            .AddSection("Section:")
            .Build();

        Assert.Contains("Task#58;: 1: Actor#44;, Actor#58;", diagram);
        Assert.Contains("section Section#58;", diagram);
    }
}
