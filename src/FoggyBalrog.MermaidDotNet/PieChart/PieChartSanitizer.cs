namespace FoggyBalrog.MermaidDotNet.PieChart;

internal static class PieChartSanitizer
{
    public static string SanitizeDataSetLabel(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static void ValidateDataSetLabel(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

}
