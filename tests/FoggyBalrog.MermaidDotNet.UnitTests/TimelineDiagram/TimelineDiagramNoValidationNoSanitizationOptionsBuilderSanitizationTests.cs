namespace FoggyBalrog.MermaidDotNet.UnitTests.TimelineDiagram;

public class TimelineDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .TimelineDiagram("Title:", options: _options)
            .AddSection("Section:")
            .AddEvents("Period:", "Event:")
            .Build();

        Assert.Contains("title Title:", diagram);
        Assert.Contains("section Section:", diagram);
        Assert.Contains("Period: : Event:", diagram);
        Assert.DoesNotContain("#58;", diagram);
    }
}
