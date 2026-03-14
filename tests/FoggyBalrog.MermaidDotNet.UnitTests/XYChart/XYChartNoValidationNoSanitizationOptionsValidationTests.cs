namespace FoggyBalrog.MermaidDotNet.UnitTests.XYChart;

public class XYChartNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void Constructor_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .XYChart(title: "   ", options: _options)
            .Build();
    }

    [Fact]
    public void WithCategoricalXAxis_DoesNotThrowIfCategoriesIsEmpty()
    {
        Mermaid
            .XYChart(options: _options)
            .WithCategoricalXAxis([])
            .Build();
    }
}
