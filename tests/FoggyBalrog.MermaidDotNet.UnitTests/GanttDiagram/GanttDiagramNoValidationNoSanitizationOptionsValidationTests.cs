namespace FoggyBalrog.MermaidDotNet.UnitTests.GanttDiagram;

public class GanttDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void GanttDiagram_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(title: " ", options: _options)
            .Build();
    }

    [Fact]
    public void GanttDiagram_DoesNotThrowIfDateFormatIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(dateFormat: " ", options: _options)
            .Build();
    }

    [Fact]
    public void GanttDiagram_DoesNotThrowIfTodayMarkerCssIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(todayMarkerCss: " ", options: _options)
            .Build();
    }

    [Fact]
    public void GanttDiagram_DoesNotThrowIfTodayMarkerCssIsNotNullWhenHideTodayMarkerIsTrue()
    {
        Mermaid
            .GanttDiagram(hideTodayMarker: true, todayMarkerCss: " ", options: _options)
            .Build();
    }

    [Fact]
    public void AddTask1_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask(" ", DateTimeOffset.Now, DateTimeOffset.Now, out var _)
            .Build();
    }

    [Fact]
    public void AddTask2_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask(" ", DateTimeOffset.Now, TimeSpan.FromDays(1), out var _)
            .Build();
    }

    [Fact]
    public void AddTask3_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddTask(" ", t1, DateTimeOffset.Now, out var _)
            .Build();
    }

    [Fact]
    public void AddTask3_DoesNotThrowIfAfterTaskIsForeign()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t2", t1, DateTimeOffset.Now, out var _)
            .Build();
    }

    [Fact]
    public void AddTask4_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddTask(" ", t1, TimeSpan.FromDays(1), out var _)
            .Build();
    }

    [Fact]
    public void AddTask4_DoesNotThrowIfAfterTaskIsForeign()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t2", t1, TimeSpan.FromDays(1), out var _)
            .Build();
    }

    [Fact]
    public void AddTask5_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddTask(" ", DateTimeOffset.Now, t1, out var _)
            .Build();
    }

    [Fact]
    public void AddTask5_DoesNotThrowIfUntilTaskIsForeign()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t2", DateTimeOffset.Now, t1, out var _)
            .Build();
    }

    [Fact]
    public void AddTask6_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
            .AddTask(" ", t1, t2, out var _)
            .Build();
    }

    [Fact]
    public void AddTask6_DoesNotThrowIfAfterTaskIsForeign()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
            .AddTask("t4", t1, t2, out var _)
            .Build();
    }

    [Fact]
    public void AddTask6_DoesNotThrowIfUntilTaskIsForeign()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t2", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t2)
            .AddTask("t4", t2, t1, out var _)
            .Build();
    }

    [Fact]
    public void AddSection_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddSection(" ")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfTaskIsForeign()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .GanttDiagram(options: _options)
            .AddCallback(t1, "functionName")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfFunctionNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddCallback(t1, " ")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfTaskIsForeign()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1);

        Mermaid
            .GanttDiagram(options: _options)
            .AddHyperlink(t1, "uri")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfUriIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddTask("t1", DateTimeOffset.Now, TimeSpan.FromDays(1), out var t1)
            .AddHyperlink(t1, " ")
            .Build();
    }

    [Fact]
    public void AddVerticalMarker_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GanttDiagram(options: _options)
            .AddVerticalMarker(" ", DateTimeOffset.Now)
            .Build();
    }
}
