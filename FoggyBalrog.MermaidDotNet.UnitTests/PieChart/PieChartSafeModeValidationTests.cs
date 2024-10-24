namespace FoggyBalrog.MermaidDotNet.UnitTests.PieChart;

public class PieChartSafeModeValidationTests
{
    [Fact]
    public void PieChartBuilder_ThrowsIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.PieChart(title: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void PieChartBuilder_ThrowsIfTextPositionIsOutOfRange()
    {
        var exception1 = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.PieChart(textPosition: -0.1);
        });

        var exception2 = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.PieChart(textPosition: 1.1);
        });

        Assert.Equal(MermaidExceptionReason.OutOfRange, exception1.Reason);
        Assert.Equal(MermaidExceptionReason.OutOfRange, exception2.Reason);
    }

    [Fact]
    public void PieChartBuilder_ThrowsIfPieOuterStrokeWidthIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.PieChart(pieOuterStrokeWidth: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddDataSet_ThrowsIfLabelIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .PieChart()
                .AddDataSet(" ", 42.7);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddDataSet_ThrowsIfValueIsStrictlyNegative()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .PieChart()
                .AddDataSet("Label", -42.7);
        });

        Assert.Equal(MermaidExceptionReason.StrictlyNegative, exception.Reason);
    }
}
