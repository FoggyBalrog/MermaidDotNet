namespace FoggyBalrog.MermaidDotNet.UnitTests.PieChart;

public class PieChartValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .PieChart(options: _options)
            .AddDataSet("a\\b", 1)
            .Build();

        Assert.Contains("\"a#92;b\" : 1", diagram);
    }
}
