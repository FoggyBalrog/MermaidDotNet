using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration;

internal static class FrontmatterGenerator
{
    public static string Generate(
        string? title,
        MermaidConfig? config)
    {
        if (title is null && (config is null || config == new MermaidConfig()))
        {
            return string.Empty;
        }

        var builder = new StringBuilder();
        builder.AppendLine("---");

        if (title is not null)
        {
            builder.AppendLine($"title: {title}");
        }

        if (config is not null && config != new MermaidConfig())
        {
            var serializer = new SerializerBuilder()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
            .WithIndentedSequences()
            .Build();

            string yaml = serializer.Serialize(new { config });

            builder.Append(yaml);
        }

        builder.AppendLine("---");
        return builder.ToString();
    }
}
