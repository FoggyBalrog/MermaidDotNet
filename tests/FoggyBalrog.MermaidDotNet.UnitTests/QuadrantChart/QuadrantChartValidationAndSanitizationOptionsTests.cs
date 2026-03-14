namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class QuadrantChartValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .QuadrantChart(quadrant1: "Q<1>", options: _options)
            .SetXAxisLabel("L<", "R>")
            .AddPoint("P:<>", 0.1, 0.2)
            .Build();

        Assert.Contains("quadrant-1 Q#60;1#62;", diagram);
        Assert.Contains("x-axis L#60; --> R#62;", diagram);
        Assert.Contains("P#58;#60;#62;: [0.1, 0.2]", diagram);
    }
}
