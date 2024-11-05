namespace FoggyBalrog.MermaidDotNet.UnitTests.QuadrantChart;

public class QuadrantChartSafeModeValidationTests
{
    [Fact]
    public void QuadrantChartBuilder_ThrowsIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart(" ", null, "Q1", "Q2", "Q3", "Q4");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void QuadrantChartBuilder_ThrowsIfQuadrant1IsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart("Title", null, " ", "Q2", "Q3", "Q4");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void QuadrantChartBuilder_ThrowsIfQuadrant2IsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart("Title", null, "Q1", " ", "Q3", "Q4");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void QuadrantChartBuilder_ThrowsIfQuadrant3IsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart("Title", null, "Q1", "Q2", " ", "Q4");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void QuadrantChartBuilder_ThrowsIfQuadrant4IsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart("Title", null, "Q1", "Q2", "Q3", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void SetXAxisLabel_ThrowsIfLeftIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .SetXAxisLabel(" ", "Right");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void SetXAxisLabel_ThrowsIfRightIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .SetXAxisLabel("Left", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void SetYAxisLabel_ThrowsIfBottomIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .SetYAxisLabel(" ", "Top");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void SetYAxisLabel_ThrowsIfTopIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .SetYAxisLabel("Bottom", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddPoint_ThrowsIfLabelIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .AddPoint(" ", 0.5, 0.5);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddPoint_ThrowsIfXBelowLowerBound()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .AddPoint("Label", -0.1, 0.5);
        });

        Assert.Equal(MermaidExceptionReason.OutOfRange, exception.Reason);
    }

    [Fact]
    public void AddPoint_ThrowsIfXAboveUpperBound()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .AddPoint("Label", 1.1, 0.5);
        });

        Assert.Equal(MermaidExceptionReason.OutOfRange, exception.Reason);
    }

    [Fact]
    public void AddPoint_ThrowsIfYBelowLowerBound()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .AddPoint("Label", 0.5, -0.1);
        });

        Assert.Equal(MermaidExceptionReason.OutOfRange, exception.Reason);
    }

    [Fact]
    public void AddPoint_ThrowsIfYAboveUpperBound()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .AddPoint("Label", 0.5, 1.1);
        });

        Assert.Equal(MermaidExceptionReason.OutOfRange, exception.Reason);
    }

    [Fact]
    public void AddPoint_ThrowsIfCssIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .AddPoint("Label", 0.5, 0.5, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddPoint_ThrowsIfCssClassIsForeign()
    {
        Mermaid
            .QuadrantChart()
            .DefineCssClass("class", "css", out var cssClass);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .AddPoint("Label", 0.5, 0.5, cssClass: cssClass);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void DefineCssClass_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .DefineCssClass(" ", "css", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void DefineCssClass_ThrowsIfCssIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .QuadrantChart()
                .DefineCssClass("foo", " ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}
