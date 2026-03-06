namespace FoggyBalrog.MermaidDotNet.GitGraph;

internal static class GitGraphSanitizer
{
    public static string SanitizeBranchName(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, ' ', ':');
    }

    public static string SanitizeCommitOrTag(string input)
    {
        return CoreSanitizer.SanitizeMultilineText(input, '"', '\\');
    }

    public static void ValidateBranchName(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters([' ', ':', '\r', '\n']);
    }

    public static void ValidateCommitOrTag(string input)
    {
        input.ThrowIfWhiteSpace();
        input.ThrowIfContainsInvalidCharacters(['"', '\\', '\r', '\n']);
    }

}
