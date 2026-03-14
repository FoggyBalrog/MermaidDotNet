namespace FoggyBalrog.MermaidDotNet.XYChart;

internal static class XYChartSanitizer
{
    public static string SanitizeAxisTitle(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static string SanitizeCategory(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static void ValidateAxisTitle(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

    public static void ValidateCategory(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

}
