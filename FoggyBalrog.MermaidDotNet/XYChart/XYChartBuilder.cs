using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.XYChart.Model;

namespace FoggyBalrog.MermaidDotNet.XYChart;

public class XYChartBuilder
{
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly XYChartOrientation? _orientation;
    private readonly bool _isSafe;
    private readonly ICollection<XYChartSeries> _series = [];
    private AbstractAxisInfo? _xAxis;
    private AbstractAxisInfo? _yAxis;

    internal XYChartBuilder(string? title, MermaidConfig? config, XYChartOrientation? orientation, bool isSafe)
    {
        if (isSafe)
        {
            title?.ThrowIfWhiteSpace();
        }

        _title = title;
        _config = config;
        _orientation = orientation;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Sets the title for the X axis.
    /// </summary>
    /// /// <remarks>
    /// If axis info is set multiple times, only the last call will take effect.
    /// </remarks>
    /// <param name="title">The title for the axis.</param>
    /// <returns>The current <see cref="XYChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public XYChartBuilder WithTitledXAxis(string title)
    {
        _xAxis = new TitledAxisInfo(title);
        return this;
    }

    /// <summary>
    /// Sets the X axis to be numeric.
    /// </summary>
    /// <remarks>
    /// If axis info is set multiple times, only the last call will take effect.
    /// </remarks>
    /// <param name="min">The minimum value for the axis.</param>
    /// <param name="max">The maximum value for the axis.</param>
    /// <param name="title">An optional title for the axis.</param>
    /// <returns>The current <see cref="XYChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public XYChartBuilder WithNumericXAxis(double min, double max, string? title = null)
    {
        _xAxis = new NumericAxisInfo(title, min, max);
        return this;
    }

    /// <summary>
    /// Sets the X axis to be categorical.
    /// </summary>
    /// <remarks>
    /// If axis info is set multiple times, only the last call will take effect.
    /// </remarks>
    /// <param name="categories">The categories for the axis.</param>
    /// <param name="title">An optional title for the axis.</param>
    /// <returns>The current <see cref="XYChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public XYChartBuilder WithCategoricalXAxis(ICollection<string> categories, string? title = null)
    {
        if (_isSafe)
        {
            categories.ThrowIfEmpty();
        }

        _xAxis = new CategoricalAxisInfo(title, categories);

        return this;
    }

    /// <summary>
    /// Sets the title for the Y axis.
    /// </summary>
    /// /// <remarks>
    /// If axis info is set multiple times, only the last call will take effect.
    /// </remarks>
    /// <param name="title">The title for the axis.</param>
    /// <returns>The current <see cref="XYChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public XYChartBuilder WithTitledYAxis(string title)
    {
        _yAxis = new TitledAxisInfo(title);
        return this;
    }

    /// <summary>
    /// Sets the Y axis to be numeric.
    /// </summary>
    /// <remarks>
    /// If axis info is set multiple times, only the last call will take effect.
    /// </remarks>
    /// <param name="min">The minimum value for the axis.</param>
    /// <param name="max">The maximum value for the axis.</param>
    /// <param name="title">An optional title for the axis.</param>
    /// <returns>The current <see cref="XYChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public XYChartBuilder WithNumericYAxis(double min, double max, string? title = null)
    {
        _yAxis = new NumericAxisInfo(title, min, max);
        return this;
    }

    /// <summary>
    /// Adds a bar series to the chart.
    /// </summary>
    /// <param name="data">The data points for the series.</param>
    /// <returns>The current <see cref="XYChartBuilder"/> instance.</returns>
    public XYChartBuilder AddBarSeries(ICollection<double> data)
    {
        _series.Add(new XYChartSeries(data, XYChartSeriesType.Bar));
        return this;
    }

    /// <summary>
    /// Adds a line series to the chart.
    /// </summary>
    /// <param name="data">The data points for the series.</param>
    /// <returns>The current <see cref="XYChartBuilder"/> instance.</returns>
    public XYChartBuilder AddLineSeries(ICollection<double> data)
    {
        _series.Add(new XYChartSeries(data, XYChartSeriesType.Line));
        return this;
    }

    /// <summary>
    /// Builds the mermaid code for the XY chart.
    /// </summary>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        string orientationString = _orientation switch
        {
            XYChartOrientation.Horizontal => " horizontal",
            XYChartOrientation.Vertical => " vertical",
            _ => string.Empty
        };

        builder.AppendLine($"xychart{orientationString}");

        if (_xAxis is not null)
        {
            BuildSeries(builder, "x-axis", _xAxis);
        }

        if (_yAxis is not null)
        {
            BuildSeries(builder, "y-axis", _yAxis);
        }

        foreach (var series in _series)
        {
            BuildSeries(builder, series);
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();

    }

    private static void BuildSeries(StringBuilder builder, string name, AbstractAxisInfo info)
    {
        string optionalTitle = $"{(info.Title is not null ? $"\"{info.Title}\" " : string.Empty)}";

        switch (info)
        {
            case TitledAxisInfo:
                builder.AppendLine($"{name} \"{info.Title}\"");
                break;

            case NumericAxisInfo numericInfo:
                builder.AppendLine($"{name} {optionalTitle}{numericInfo.Min} --> {numericInfo.Max}");
                break;

            case CategoricalAxisInfo categoricalInfo:
                builder.AppendLine($"{name} {optionalTitle}[\"{string.Join("\", \"", categoricalInfo.Categories)}\"]");
                break;

            default:
                throw MermaidException.InvalidOperation($"Unknown axis info type '{info.GetType().FullName}'.");
        }
    }

    private static void BuildSeries(StringBuilder builder, XYChartSeries series)
    {
        string seriesTypeString = series.Type switch
        {
            XYChartSeriesType.Bar => "bar",
            XYChartSeriesType.Line => "line",
            _ => throw MermaidException.InvalidOperation("Unknown series type.")
        };

        builder.AppendLine($"{seriesTypeString} [{string.Join(", ", series.Data)}]");
    }
}