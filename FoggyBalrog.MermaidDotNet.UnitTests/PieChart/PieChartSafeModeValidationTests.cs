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
