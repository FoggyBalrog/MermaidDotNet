namespace FoggyBalrog.MermaidDotNet;

internal static class StringExtensions
{
    public static string Repeat(this string input, uint count)
    {
        var inputSpan = input.AsSpan();
        var outputSpan = new Span<char>(new char[inputSpan.Length * count]);

        for (int i = 0; i < count; i++)
        {
            inputSpan.CopyTo(outputSpan.Slice(i * inputSpan.Length, inputSpan.Length));
        }

        return outputSpan.ToString();
    }
}
