namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public sealed class MermaidParserIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task InvalidMermaidSyntax_FailsParsing()
    {
        const string invalidDiagram = """
                                      flowchart TB
                                          A -->
                                      """;

        var result = await toolingFixture.ValidateDiagramAsync(invalidDiagram);

        Assert.True(result.ExitCode != 0, "Expected Mermaid parser validation to fail for invalid syntax.");
        Assert.False(string.IsNullOrWhiteSpace(result.StandardError));
    }
}
