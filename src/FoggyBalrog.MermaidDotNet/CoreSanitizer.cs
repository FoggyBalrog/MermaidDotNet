namespace FoggyBalrog.MermaidDotNet;

internal static class CoreSanitizer
{
    public static Dictionary<char, string> Codes { get; } = new()
    {
        [' ']  = "#32;",
        ['!']  = "#33;",
        ['"']  = "#34;",
        ['#']  = "#35;",
        ['$']  = "#36;",
        ['%']  = "#37;",
        ['&']  = "#38;",
        ['\''] = "#39;",
        ['(']  = "#40;",
        [')']  = "#41;",
        ['*']  = "#42;",
        ['+']  = "#43;",
        [',']  = "#44;",
        ['-']  = "#45;",
        ['.']  = "#46;",
        ['/']  = "#47;",
        [':']  = "#58;",
        [';']  = "#59;",
        ['<']  = "#60;",
        ['=']  = "#61;",
        ['>']  = "#62;",
        ['?']  = "#63;",
        ['@']  = "#64;",
        ['[']  = "#91;",
        ['\\'] = "#92;",
        [']']  = "#93;",
        ['^']  = "#94;",
        ['`']  = "#96;",
        ['{']  = "#123;",
        ['|']  = "#124;",
        ['}']  = "#125;",
        ['~']  = "#126;",
    };

    public static string SanitizeUri(string input)
    {
        string sanitized = Uri.EscapeUriString(input);

        if (!input.EndsWith('/') && sanitized.EndsWith('/'))
        {
            sanitized = sanitized[..^1];
        }

        return sanitized;
    }

    public static string SanitizeText(string input, params char[] escapedCharacters)
    {
        if (escapedCharacters.Length == 0)
        {
            return input;
        }

        return string.Join("", input.Select(c => escapedCharacters.Contains(c) ? Codes[c] : c.ToString()));
    }

    public static string SanitizeMultilineText(string input, params char[] escapedCharacters)
    {
        return SanitizeText(input, escapedCharacters)
            .Replace("\r\n", "<br/>")
            .Replace("\n", "<br/>");
    }
}
