namespace FoggyBalrog.MermaidDotNet.UserJourneyDiagram;

internal static class UserJourneyDiagramSanitizer
{
    public static string SanitizeTaskDescription(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeSectionDescription(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':');
    }

    public static string SanitizeActor(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ':', ',');
    }

    public static void ValidateTaskDescription(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateSectionDescription(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', '\r', '\n']);
    }

    public static void ValidateActor(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([':', ',', '\r', '\n']);
    }

}
