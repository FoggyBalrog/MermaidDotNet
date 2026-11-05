namespace FoggyBalrog.MermaidDotNet.QuadrantChart;

internal static class QuadrantChartSanitizer
{
    public static string SanitizeAxisLabel(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '<', '>');
    }

    public static string SanitizeQuadrantLabel(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '<', '>');
    }

    public static string SanitizePointLabel(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':', '<', '>');
    }

    public static void ValidateAxisLabel(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['<', '>', '\r', '\n']);
    }

    public static void ValidateQuadrantLabel(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['<', '>', '\r', '\n']);
    }

    public static void ValidatePointLabel(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '<', '>', '\r', '\n']);
    }

}
