namespace FoggyBalrog.MermaidDotNet.UnitTests.QuadrantChart;

public class QuadrantChartNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(" ", null, "Q1", "Q2", "Q3", "Q4", options: _options)
            .Build();
    }

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfQuadrant1IsWhiteSpace()
    {
        Mermaid
            .QuadrantChart("Title", null, " ", "Q2", "Q3", "Q4", options: _options)
            .Build();
    }

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfQuadrant2IsWhiteSpace()
    {
        Mermaid
            .QuadrantChart("Title", null, "Q1", " ", "Q3", "Q4", options: _options)
            .Build();
    }

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfQuadrant3IsWhiteSpace()
    {
        Mermaid
            .QuadrantChart("Title", null, "Q1", "Q2", " ", "Q4", options: _options)
            .Build();
    }

    [Fact]
    public void QuadrantChartBuilder_DoesNotThrowIfQuadrant4IsWhiteSpace()
    {
        Mermaid
            .QuadrantChart("Title", null, "Q1", "Q2", "Q3", " ", options: _options)
            .Build();
    }

    [Fact]
    public void SetXAxisLabel_DoesNotThrowIfLeftIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .SetXAxisLabel(" ", "Right")
            .Build();
    }

    [Fact]
    public void SetXAxisLabel_DoesNotThrowIfRightIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .SetXAxisLabel("Left", " ")
            .Build();
    }

    [Fact]
    public void SetYAxisLabel_DoesNotThrowIfBottomIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .SetYAxisLabel(" ", "Top")
            .Build();
    }

    [Fact]
    public void SetYAxisLabel_DoesNotThrowIfTopIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .SetYAxisLabel("Bottom", " ")
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfLabelIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .AddPoint(" ", 0.5, 0.5)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfXBelowLowerBound()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .AddPoint("Label", -0.1, 0.5)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfXAboveUpperBound()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .AddPoint("Label", 1.1, 0.5)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfYBelowLowerBound()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .AddPoint("Label", 0.5, -0.1)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfYAboveUpperBound()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .AddPoint("Label", 0.5, 1.1)
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfCssIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .AddPoint("Label", 0.5, 0.5, " ")
            .Build();
    }

    [Fact]
    public void AddPoint_DoesNotThrowIfCssClassIsForeign()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .DefineCssClass("class", "css", out var cssClass);

        Mermaid
            .QuadrantChart(options: _options)
            .AddPoint("Label", 0.5, 0.5, cssClass: cssClass)
            .Build();
    }

    [Fact]
    public void DefineCssClass_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .DefineCssClass(" ", "css", out var _)
            .Build();
    }

    [Fact]
    public void DefineCssClass_DoesNotThrowIfCssIsWhiteSpace()
    {
        Mermaid
            .QuadrantChart(options: _options)
            .DefineCssClass("foo", " ", out var _)
            .Build();
    }
}
