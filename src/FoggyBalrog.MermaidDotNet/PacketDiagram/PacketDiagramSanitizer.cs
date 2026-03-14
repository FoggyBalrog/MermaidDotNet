namespace FoggyBalrog.MermaidDotNet.PacketDiagram;

internal static class PacketDiagramSanitizer
{
    public static string SanitizeFieldDescription(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static void ValidateFieldDescription(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

}
