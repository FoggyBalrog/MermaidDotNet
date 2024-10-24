using System.Globalization;
using System.Text;
using FoggyBalrog.MermaidDotNet.PieChart.Model;

namespace FoggyBalrog.MermaidDotNet.PieChart;

/// <summary>
/// A builder for pie charts.
/// </summary>
public class PieChartBuilder
{
    private readonly bool _displayValuesOnLegend;
    private readonly string? _title;
    private readonly double? _textPosition;
    private readonly string? _pieOuterStrokeWidth;
    private readonly bool _isSafe;
    private readonly List<DataSet> _dataSets = [];

    internal PieChartBuilder(
        bool displayValuesOnLegend,
        string? title,
        double? textPosition,
        string? pieOuterStrokeWidth,
        bool isSafe)
    {
        if (isSafe)
        {
            title.ThrowIfWhiteSpace();
            textPosition?.ThrowIfOutOfRange(0, 1);
            pieOuterStrokeWidth.ThrowIfWhiteSpace();
        }

        _displayValuesOnLegend = displayValuesOnLegend;
        _title = title;
        _textPosition = textPosition;
        _pieOuterStrokeWidth = pieOuterStrokeWidth;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds a data set to the pie chart.
    /// </summary>
    /// <param name="label">The label of the data set.</param>
    /// <param name="value">The value of the data set.</param>
    /// <returns>The current <see cref="PieChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="label"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="value"/> is strictly negative, with the reason <see cref="MermaidExceptionReason.StrictlyNegative"/>.</exception>
    public PieChartBuilder AddDataSet(string label, double value)
    {
        if (_isSafe)
        {
            label.ThrowIfWhiteSpace();
            value.ThrowIfStrictlyNegative();
        }

        _dataSets.Add(new DataSet(label, value));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the pie chart.
    /// </summary>
    /// <returns>The Mermaid code for the pie chart.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        if (_textPosition is not null || _pieOuterStrokeWidth is not null)
        {
            builder.Append("%%{init: {");

            if (_textPosition is not null)
            {
                string invariantTextPosition = _textPosition.Value.ToString(CultureInfo.InvariantCulture);
                builder.Append($"\"pie\": {{\"textPosition\": {invariantTextPosition}}}");
            }

            if (_textPosition is not null && _pieOuterStrokeWidth is not null)
            {
                builder.Append(", ");
            }

            if (_pieOuterStrokeWidth is not null)
            {
                builder.Append($"\"themeVariables\": {{\"pieOuterStrokeWidth\": \"{_pieOuterStrokeWidth}\"}}");
            }

            builder.Append("}}%%");
            builder.AppendLine();
        }

        builder.Append("pie");

        if (_displayValuesOnLegend)
        {
            builder.Append(" showData");
        }

        if (!string.IsNullOrWhiteSpace(_title))
        {
            builder.Append($" title {_title}");
        }

        builder.AppendLine();

        foreach (DataSet? dataSet in _dataSets)
        {
            string formattedValue = dataSet.Value.ToString(CultureInfo.InvariantCulture);
            builder.AppendLine($"{Shared.Indent}\"{dataSet.Label}\" : {formattedValue}");
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
