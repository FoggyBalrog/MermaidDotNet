namespace FoggyBalrog.MermaidDotNet.SequenceDiagram;

internal static class SequenceDiagramSanitizer
{
    public static string SanitizeParticipantName(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '-', '<', '>', ':', ';', ',', '#');
    }

    public static string SanitizeMessageDescription(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ';', '#');
    }

    public static string SanitizeLinkTitle(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ';', '#', '@');
    }

    public static string SanitizeLinkUri(string input)
    {
        return CoreSanitizer.SanitizeUri(input);
    }

    public static void ValidateParticipantName(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['-', '<', '>', ':', ';', ',', '#', '\r', '\n']);
    }

    public static void ValidateMessageDescription(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([';', '#', '\r', '\n']);
    }

    public static void ValidateLinkTitle(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([';', '#', '@', '\r', '\n']);
    }

    public static void ValidateLinkUri(string input)
    {
        input.ThrowIfInvalidUri();
    }
}
