namespace FoggyBalrog.MermaidDotNet.UnitTests.XYChart;

public class XYChartNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .XYChart(options: _options)
            .WithCategoricalXAxis(["C\\D"], "T\\X")
            .WithTitledYAxis("Y\\Z")
            .AddBarSeries([1])
            .Build();

        Assert.Contains("x-axis \"T\\X\" [\"C\\D\"]", diagram);
        Assert.Contains("y-axis \"Y\\Z\"", diagram);
        Assert.DoesNotContain("#92;", diagram);
    }
}
