namespace FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram;

internal static class EntityRelationshipDiagramSanitizer
{
    public static string SanitizeEntityName(string input)
    {
        return CoreSanitizer.SanitizeText(
            input,
            '"', '\'', '<', '>', '{', '}', '`', '!', '?', '#', '@', '$', '/', '\\', '|', '&', '(', ')',
            '=', '+', '~', ':', ';', '[', ']', '^', '.', ',');
    }

    public static string SanitizeEntityAttributeComment(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static string SanitizeRelationshipLabel(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static void ValidateEntityName(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(
            ['"', '\'', '<', '>', '{', '}', '`', '!', '?', '#', '@', '$', '/', '\\', '|', '&', '(', ')', '=', '+', '~', ':', ';', '[', ']', '^', '.', ',']);
    }

    public static void ValidateEntityAttributeComment(string input)
    {
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

    public static void ValidateRelationshipLabel(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

}
