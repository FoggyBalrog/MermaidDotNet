using System.Text;
using FoggyBalrog.MermaidDotNet.QuadrantChart.Model;

namespace FoggyBalrog.MermaidDotNet.QuadrantChart;

internal static class StyleConfigurationExtensions
{
    public static string ToMermaidConfigString(this StyleConfiguration styleConfig)
    {
        var sb = new StringBuilder("%%{init: {");
        bool hasPreviousSection = false;

        if (styleConfig.ChartConfigurations != null)
        {
            sb.Append("\"quadrantChart\": {");
            AppendProperties(sb, styleConfig.ChartConfigurations);
            sb.Append("}");
            hasPreviousSection = true;
        }

        if (styleConfig.ThemeVariables != null)
        {
            if (hasPreviousSection)
            {
                sb.Append(", ");
            }
            sb.Append("\"themeVariables\": {");
            AppendProperties(sb, styleConfig.ThemeVariables);
            sb.Append("}");
        }

        sb.Append("}}%%");
        return sb.ToString();
    }

    private static void AppendProperties(StringBuilder sb, object config)
    {
        var properties = config.GetType().GetProperties();
        bool hasPreviousProperty = false;

        foreach (var property in properties)
        {
            var value = property.GetValue(config);
            if (value != null)
            {
                if (hasPreviousProperty)
                {
                    sb.Append(", ");
                }

                sb.Append($"\"{property.Name[0].ToString().ToLower() + property.Name.Substring(1)}\": ");

                if (value is string)
                {
                    sb.Append($"\"{value}\"");
                }
                else
                {
                    sb.Append($"{value}");
                }

                hasPreviousProperty = true;
            }
        }
    }
}
