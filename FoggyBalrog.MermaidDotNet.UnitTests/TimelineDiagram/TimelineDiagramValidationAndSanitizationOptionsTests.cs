namespace FoggyBalrog.MermaidDotNet.UnitTests.TimelineDiagram;

public class TimelineDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .TimelineDiagram("Title:", options: _options)
            .AddSection("Section:")
            .AddEvents("Period:", "Event:")
            .Build();

        Assert.Contains("title Title#58;", diagram);
        Assert.Contains("section Section#58;", diagram);
        Assert.Contains("Period#58; : Event#58;", diagram);
    }
}
