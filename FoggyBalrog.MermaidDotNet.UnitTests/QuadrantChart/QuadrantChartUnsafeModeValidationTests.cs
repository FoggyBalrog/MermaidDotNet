namespace FoggyBalrog.MermaidDotNet.UnitTests.QuadrantChart;

public class QuadrantChartUnsafeModeValidationTests
{
    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart(" ", null, "Q1", "Q2", "Q3", "Q4")
            .Build();
    }

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfQuadrant1IsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart("Title", null, " ", "Q2", "Q3", "Q4")
            .Build();
    }

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfQuadrant2IsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart("Title", null, "Q1", " ", "Q3", "Q4")
            .Build();
    }

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfQuadrant3IsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart("Title", null, "Q1", "Q2", " ", "Q4")
            .Build();
    }

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfQuadrant4IsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart("Title", null, "Q1", "Q2", "Q3", " ")
            .Build();
    }

    [Fact]
    public void SetXAxisLabel_DoesNotThrowIfLeftIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .SetXAxisLabel(" ", "Right")
            .Build();
    }

    [Fact]
    public void SetXAxisLabel_DoesNotThrowIfRightIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .SetXAxisLabel("Left", " ")
            .Build();
    }

    [Fact]
    public void SetYAxisLabel_DoesNotThrowIfBottomIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .SetYAxisLabel(" ", "Top")
            .Build();
    }

    [Fact]
    public void SetYAxisLabel_DoesNotThrowIfTopIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .SetYAxisLabel("Bottom", " ")
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfLabelIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .AddPoint(" ", 0.5, 0.5)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfXBelowLowerBound()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .AddPoint("Label", -0.1, 0.5)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfXAboveUpperBound()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .AddPoint("Label", 1.1, 0.5)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfYBelowLowerBound()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .AddPoint("Label", 0.5, -0.1)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfYAboveUpperBound()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .AddPoint("Label", 0.5, 1.1)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfCssIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .AddPoint("Label", 0.5, 0.5, " ")
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfCssClassIsForeign()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .DefineCssClass("class", "css", out var cssClass);

        Mermaid
            .Unsafe
            .QuadrantChart()
            .AddPoint("Label", 0.5, 0.5, cssClass: cssClass)
            .Build();
    }

    [Fact]
    public void DefineCssClass_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .DefineCssClass(" ", "css", out var _)
            .Build();
    }

    [Fact]
    public void DefineCssClass_DoesNotThrowIfCssIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .QuadrantChart()
            .DefineCssClass("foo", " ", out var _)
            .Build();
    }
}
