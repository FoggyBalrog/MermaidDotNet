namespace FoggyBalrog.MermaidDotNet.UnitTests.GanttDiagram;

public class GanttDiagramSafeModeValidationTests
{
    [Fact]
    public void GanttDiagram_ThrowsIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram(title: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void GanttDiagram_ThrowsIfDateFormatIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram(dateFormat: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void GanttDiagram_ThrowsIfTodayMarkerCssIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram(todayMarkerCss: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void GanttDiagram_ThrowsIfTodayMarkerCssIsNotNullWhenHideTodayMarkerIsTrue()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram(hideTodayMarker: true, todayMarkerCss: "stroke: #d3d3d3; stroke-width: 2px;");
        });

        Assert.Equal(MermaidExceptionReason.InvalidConfiguration, exception.Reason);
    }

    [Fact]
    public void AddTask1_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask(" ", DateTimeOffset.Now, DateTimeOffset.Now, out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTask2_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask(" ", DateTimeOffset.Now, TimeSpan.FromDays(1), out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTask3_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
                .AddTask(" ", t1, DateTimeOffset.Now, out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTask3_ThrowsIfAfterTaskIsForeign()
    {
        Mermaid
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t2", t1, DateTimeOffset.Now, out var _);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddTask4_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
                .AddTask(" ", t1, TimeSpan.FromDays(1), out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTask4_ThrowsIfAfterTaskIsForeign()
    {
        Mermaid
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t2", t1, TimeSpan.FromDays(1), out var _);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddTask5_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
                .AddTask(" ", DateTimeOffset.Now, t1, out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTask5_ThrowsIfUntilTaskIsForeign()
    {
        Mermaid
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t2", DateTimeOffset.Now, t1, out var _);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddTask6_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
                .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
                .AddTask(" ", t1, t2, out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTask6_ThrowsIfAfterTaskIsForeign()
    {
        Mermaid
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
                .AddTask("t4", t1, t2, out var _);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddTask6_ThrowsIfUntilTaskIsForeign()
    {
        Mermaid
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
                .AddTask("t4", t2, t1, out var _);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddSection_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddSection(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddCallback_ThrowsIfTaskIsForeign()
    {
        Mermaid
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddCallback(t1, "functionName");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddCallback_ThrowsIfFunctionNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
                .AddCallback(t1, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddHyperlink_ThrowsIfTaskIsForeign()
    {
        Mermaid
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddHyperlink(t1, "uri");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddHyperlink_ThrowsIfUriIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
                .AddHyperlink(t1, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddVerticalMarker_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GanttDiagram()
                .AddVerticalMarker(" ", DateTimeOffset.Now);
        });
        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}
