namespace FoggyBalrog.MermaidDotNet.UnitTests.PieChart;

public class PieChartUnsafeModeValidationTests
{
    [Fact]
    public void PieChartBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .PieChart(title: " ")
            .Build();
    }

    [Fact]
    public void AddDataSet_DoesNotThrowIfLabelIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .PieChart()
            .AddDataSet(" ", 42.7)
            .Build();
    }

    [Fact]
    public void AddDataSet_DoesNotThrowIfValueIsStrictlyNegative()
    {
        Mermaid
            .Unsafe
            .PieChart()
            .AddDataSet("Label", -42.7)
            .Build();
    }
}
