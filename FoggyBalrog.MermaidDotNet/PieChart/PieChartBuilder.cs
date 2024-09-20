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
    private readonly bool _isSafe;
    private readonly List<DataSet> _dataSets = [];

    internal PieChartBuilder(bool displayValuesOnLegend, string? title, bool isSafe)
    {
        if (isSafe)
        {
            title.ThrowIfWhiteSpace();
        }

        _displayValuesOnLegend = displayValuesOnLegend;
        _title = title;
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
