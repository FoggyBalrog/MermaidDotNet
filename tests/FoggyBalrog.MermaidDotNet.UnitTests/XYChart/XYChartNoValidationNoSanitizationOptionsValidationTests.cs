namespace FoggyBalrog.MermaidDotNet.UnitTests.XYChart;

public class XYChartNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void Constructor_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .XYChart(title: "   ", options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .Build();
    }

    [Fact]
    public void WithCategoricalXAxis_DoesNotThrowIfCategoriesIsEmpty()
    {
        Mermaid
            .XYChart(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .WithCategoricalXAxis([])
            .Build();
    }
}
