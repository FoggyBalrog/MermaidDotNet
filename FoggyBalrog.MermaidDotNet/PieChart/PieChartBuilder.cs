using System.Globalization;
using System.Text;
using FoggyBalrog.MermaidDotNet.PieChart.Model;

namespace FoggyBalrog.MermaidDotNet.PieChart;

public class PieChartBuilder
{
    private readonly bool _displayValuesOnLegend;
    private readonly string? _title;
    private readonly List<DataSet> _dataSets = [];

    internal PieChartBuilder(bool displayValuesOnLegend, string? title)
    {
        _displayValuesOnLegend = displayValuesOnLegend;
        _title = title;
    }

    public PieChartBuilder AddDataSet(string label, double value)
    {
        _dataSets.Add(new DataSet(label, value));
        return this;
    }

    public string Build()
    {
        const string indent = "    ";
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
            builder.AppendLine($"{indent}\"{dataSet.Label}\" : {formattedValue}");
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
