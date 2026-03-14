namespace FoggyBalrog.MermaidDotNet.SankeyDiagram;

internal static class SankeyDiagramSanitizer
{
    public static string SanitizeFlowText(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ',');
    }

    public static void ValidateFlowText(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([',', '\r', '\n']);
    }
}
