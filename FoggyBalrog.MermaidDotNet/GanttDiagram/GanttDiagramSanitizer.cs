namespace FoggyBalrog.MermaidDotNet.GanttDiagram;

internal static class GanttDiagramSanitizer
{
    public static string SanitizeTaskName(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeSectionName(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeVerticalMarkerName(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeFunctionName(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ' ', '(', ')', ':', ',');
    }

    public static string SanitizeHyperlinkUri(string input)
    {
        return CoreSanitizer.SanitizeUri(input);
    }

    public static void ValidateTaskName(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateSectionName(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateVerticalMarkerName(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateFunctionName(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([' ', '(', ')', ':', ',', '\r', '\n']);
    }

    public static void ValidateHyperlinkUri(string input)
    {
        input.ThrowIfInvalidUri();
    }

}
