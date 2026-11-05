namespace FoggyBalrog.MermaidDotNet.StateDiagram;

internal static class StateDiagramSanitizer
{
    public static string SanitizeStateDescription(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':', '"', '\\');
    }

    public static string SanitizeTransitionDescription(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeNoteText(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input);
    }

    public static string SanitizeLinkUrl(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static string SanitizeLinkTooltip(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static void ValidateStateDescription(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '"', '\\', '\r', '\n']);
    }

    public static void ValidateTransitionDescription(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateNoteText(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['\r', '\n']);
    }

    public static void ValidateLinkUrl(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

    public static void ValidateLinkTooltip(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

}
