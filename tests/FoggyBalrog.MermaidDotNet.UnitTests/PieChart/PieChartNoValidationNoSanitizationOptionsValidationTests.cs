namespace FoggyBalrog.MermaidDotNet.UnitTests.PieChart;

public class PieChartNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void PieChartBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .PieChart(title: " ", options: _options)
            .Build();
    }

    [Fact]
    public void AddDataSet_DoesNotThrowIfLabelIsWhiteSpace()
    {
        Mermaid
            .PieChart(options: _options)
            .AddDataSet(" ", 42.7)
            .Build();
    }

    [Fact]
    public void AddDataSet_DoesNotThrowIfValueIsStrictlyNegative()
    {
        Mermaid
            .PieChart(options: _options)
            .AddDataSet("Label", -42.7)
            .Build();
    }
}
