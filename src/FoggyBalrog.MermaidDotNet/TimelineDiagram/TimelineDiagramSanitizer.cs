namespace FoggyBalrog.MermaidDotNet.TimelineDiagram;

internal static class TimelineDiagramSanitizer
{
    public static string SanitizeTimelineTitle(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeSectionTitle(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeTimePeriod(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeEvent(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static void ValidateTimelineTitle(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateSectionTitle(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateTimePeriod(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateEvent(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

}
