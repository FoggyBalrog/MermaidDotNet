namespace FoggyBalrog.MermaidDotNet.RequirementDiagram;

internal static class RequirementDiagramSanitizer
{
    public static string SanitizeText(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static void ValidateText(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

}
