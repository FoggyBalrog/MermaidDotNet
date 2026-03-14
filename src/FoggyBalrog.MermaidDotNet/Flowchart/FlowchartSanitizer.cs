namespace FoggyBalrog.MermaidDotNet.Flowchart;

internal static class FlowchartSanitizer
{
    public static string SanitizeHyperlinkUri(string input)
    {
        return CoreSanitizer.SanitizeUri(input);
    }

    public static string SanitizeNodeText(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static string SanitizeLinkText(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static string SanitizeTooltip(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static void ValidateHyperlinkUri(string input)
    {
        input.ThrowIfInvalidUri();
    }

    public static void ValidateNodeText(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

    public static void ValidateLinkText(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

    public static void ValidateTooltip(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

}
