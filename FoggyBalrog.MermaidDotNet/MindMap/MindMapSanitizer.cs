namespace FoggyBalrog.MermaidDotNet.MindMap;

internal static class MindMapSanitizer
{
    public static string SanitizeNodeText(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\', '`', '[', ']', '(', ')', '{', '}', ':');
    }

    public static string SanitizeIcon(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '(', ')', '"', '\'', '\\');
    }

    public static string SanitizeCssClass(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ' ', ':', '"', '\'', '\\');
    }

    public static void ValidateNodeText(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '`', '[', ']', '(', ')', '{', '}', ':', '\r', '\n']);
    }

    public static void ValidateIcon(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['(', ')', '"', '\'', '\\', '\r', '\n']);
    }

    public static void ValidateCssClass(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([' ', ':', '"', '\'', '\\', '\r', '\n']);
    }

}
