namespace FoggyBalrog.MermaidDotNet.BlockDiagram;

internal static class BlockDiagramSanitizer
{
    public static string SanitizeBlockLabel(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"');
    }

    public static string SanitizeLinkText(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"');
    }

    public static void ValidateBlockLabel(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\r', '\n']);
    }

    public static void ValidateLinkText(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\r', '\n']);
    }

}
