using FoggyBalrog.MermaidDotNet.GitGraph.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class GitGraphValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .GitGraph(options: _options)
            .Branch("feat: x", out Branch feature)
            .Checkout(feature)
            .Commit()
            .Build();

        Assert.Contains("branch feat#58;#32;x", diagram);
        Assert.Contains("checkout feat#58;#32;x", diagram);
    }
}
