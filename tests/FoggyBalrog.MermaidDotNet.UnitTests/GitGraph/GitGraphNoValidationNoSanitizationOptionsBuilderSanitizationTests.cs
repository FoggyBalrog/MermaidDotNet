using FoggyBalrog.MermaidDotNet.GitGraph.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class GitGraphNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .GitGraph(options: _options)
            .Branch("feat: x", out Branch feature)
            .Checkout(feature)
            .Commit()
            .Build();

        Assert.Contains("branch feat: x", diagram);
        Assert.Contains("checkout feat: x", diagram);
        Assert.DoesNotContain("#58;", diagram);
    }
}
