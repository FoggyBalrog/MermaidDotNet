namespace FoggyBalrog.MermaidDotNet.UnitTests.PieChart;

public class PieChartNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .PieChart(options: _options)
            .AddDataSet("a\\b", 1)
            .Build();

        Assert.Contains("\"a\\b\" : 1", diagram);
        Assert.DoesNotContain("#92;", diagram);
    }
}
