namespace FoggyBalrog.MermaidDotNet.UnitTests.GanttDiagram;

public class GanttDiagramUnsafeModeValidationTests
{
    [Fact]
    public void GanttDiagram_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram(title: " ")
            .Build();
    }

    [Fact]
    public void GanttDiagram_DoesNotThrowIfDateFormatIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram(dateFormat: " ")
            .Build();
    }

    [Fact]
    public void GanttDiagram_DoesNotThrowIfTodayMarkerCssIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram(todayMarkerCss: " ")
            .Build();
    }

    [Fact]
    public void GanttDiagram_DoesNotThrowIfTodayMarkerCssIsNotNullWhenHideTodayMarkerIsTrue()
    {
        Mermaid
            .Unsafe
            .GanttDiagram(hideTodayMarker: true, todayMarkerCss: " ")
            .Build();
    }

    [Fact]
    public void AddTask1_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask(" ", DateTimeOffset.Now, DateTimeOffset.Now, out var _)
            .Build();
    }

    [Fact]
    public void AddTask2_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask(" ", DateTimeOffset.Now, TimeSpan.FromDays(1), out var _)
            .Build();
    }

    [Fact]
    public void AddTask3_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddTask(" ", t1, DateTimeOffset.Now, out var _)
            .Build();
    }

    [Fact]
    public void AddTask3_DoesNotThrowIfAfterTaskIsForeign()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t2", t1, DateTimeOffset.Now, out var _)
            .Build();
    }

    [Fact]
    public void AddTask4_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddTask(" ", t1, TimeSpan.FromDays(1), out var _)
            .Build();
    }

    [Fact]
    public void AddTask4_DoesNotThrowIfAfterTaskIsForeign()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t2", t1, TimeSpan.FromDays(1), out var _)
            .Build();
    }

    [Fact]
    public void AddTask5_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddTask(" ", DateTimeOffset.Now, t1, out var _)
            .Build();
    }

    [Fact]
    public void AddTask5_DoesNotThrowIfUntilTaskIsForeign()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t2", DateTimeOffset.Now, t1, out var _)
            .Build();
    }

    [Fact]
    public void AddTask6_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
            .AddTask(" ", t1, t2, out var _)
            .Build();
    }

    [Fact]
    public void AddTask6_DoesNotThrowIfAfterTaskIsForeign()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
            .AddTask("t4", t1, t2, out var _)
            .Build();
    }

    [Fact]
    public void AddTask6_DoesNotThrowIfUntilTaskIsForeign()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
            .AddTask("t4", t2, t1, out var _)
            .Build();
    }

    [Fact]
    public void AddSection_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddSection(" ")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfTaskIsForeign()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddCallback(t1, "functionName")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfFunctionNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddCallback(t1, " ")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfTaskIsForeign()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddHyperlink(t1, "uri")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfUriIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddHyperlink(t1, " ")
            .Build();
    }
}
