namespace FoggyBalrog.MermaidDotNet.UnitTests.XYChart;

public class XYChartValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .XYChart(options: _options)
            .WithCategoricalXAxis(["C\\D"], "T\\X")
            .WithTitledYAxis("Y\\Z")
            .AddBarSeries([1])
            .Build();

        Assert.Contains("x-axis \"T#92;X\" [\"C#92;D\"]", diagram);
        Assert.Contains("y-axis \"Y#92;Z\"", diagram);
    }
}
