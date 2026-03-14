namespace FoggyBalrog.MermaidDotNet.ClassDiagram;

internal static class ClassDiagramSanitizer
{
    public static string SanitizeRelationshipLabel(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeClassLabel(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '<', '>');
    }

    public static string SanitizeMember(string input)
    {
        return CoreSanitizer.SanitizeText(input, '{', '}');
    }

    public static string SanitizeNote(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"');
    }

    public static string SanitizeTooltip(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"');
    }

    public static string SanitizeHyperlinkUri(string input)
    {
        return CoreSanitizer.SanitizeUri(input);
    }

    public static void ValidateRelationshipLabel(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateClassLabel(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '<', '>', '\r', '\n']);
    }

    public static void ValidateMember(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['{', '}']);
    }

    public static void ValidateNote(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\r', '\n']);
    }

    public static void ValidateTooltip(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\r', '\n']);
    }

    public static void ValidateHyperlinkUri(string input)
    {
        input.ThrowIfInvalidUri();
    }

}
