namespace FoggyBalrog.MermaidDotNet.KanbanDiagram;

internal static class KanbanDiagramSanitizer
{
    public static string SanitizeColumnTitle(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '[', ']');
    }

    public static string SanitizeTaskDescription(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '[', ']');
    }

    public static string SanitizeAssigned(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '\'', '\\');
    }

    public static string SanitizeTicket(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ',', '{', '}');
    }

    public static void ValidateColumnTitle(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['[', ']', '\r', '\n']);
    }

    public static void ValidateTaskDescription(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['[', ']', '\r', '\n']);
    }

    public static void ValidateAssigned(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['\'', '\\', '\r', '\n']);
    }

    public static void ValidateTicket(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([',', '{', '}', '\r', '\n']);
    }

}
