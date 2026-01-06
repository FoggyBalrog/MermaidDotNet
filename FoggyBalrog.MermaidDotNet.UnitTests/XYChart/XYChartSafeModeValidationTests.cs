namespace FoggyBalrog.MermaidDotNet.UnitTests.XYChart;

public class XYChartSafeModeValidationTests
{
    [Fact]
    public void Constructor_ThrowsIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .XYChart(title: "   ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void WithCategoricalXAxis_ThrowsIfCategoriesIsEmpty()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .XYChart()
                .WithCategoricalXAxis([]);
        });

        Assert.Equal(MermaidExceptionReason.EmptyCollection, exception.Reason);
    }
}
