namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class QuadrantChartNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .QuadrantChart(quadrant1: "Q<1>", options: _options)
            .SetXAxisLabel("L<", "R>")
            .AddPoint("P:<>", 0.1, 0.2)
            .Build();

        Assert.Contains("quadrant-1 Q<1>", diagram);
        Assert.Contains("x-axis L< --> R>", diagram);
        Assert.Contains("P:<>: [0.1, 0.2]", diagram);
        Assert.DoesNotContain("#60;", diagram);
    }
}
