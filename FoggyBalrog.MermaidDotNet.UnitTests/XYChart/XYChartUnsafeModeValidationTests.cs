namespace FoggyBalrog.MermaidDotNet.UnitTests.XYChart;

public class XYChartUnsafeModeValidationTests
{
    [Fact]
    public void Constructor_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .XYChart(title: "   ")
            .Build();
    }

    [Fact]
    public void WithCategoricalXAxis_DoesNotThrowIfCategoriesIsEmpty()
    {
        Mermaid
            .Unsafe
            .XYChart()
            .WithCategoricalXAxis([])
            .Build();
    }
}
