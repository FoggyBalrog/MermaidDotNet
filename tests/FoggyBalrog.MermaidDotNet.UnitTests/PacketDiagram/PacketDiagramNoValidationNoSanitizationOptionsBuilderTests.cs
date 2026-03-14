namespace FoggyBalrog.MermaidDotNet.UnitTests.PacketDiagram;

public class PacketDiagramNoValidationNoSanitizationOptionsBuilderTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .PacketDiagram(options: _options)
            .Build();

        Assert.Equal("packet", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithEndFieldFirst()
    {
        string diagram = Mermaid
            .PacketDiagram("some title", options: _options)
            .AddFieldWithEnd(10, "foo")
            .AddFieldWithBits(5, "bar")
            .AddFieldWithEnd(25, "baz")
            .Build();

        Assert.Equal(@"---
title: some title
---
packet
0-10: ""foo""
+5: ""bar""
16-25: ""baz""", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithBitsFieldFirst()
    {
        string diagram = Mermaid
            .PacketDiagram("some title", options: _options)
            .AddFieldWithBits(5, "foo")
            .AddFieldWithEnd(10, "bar")
            .Build();

        Assert.Equal(@"---
title: some title
---
packet
+5: ""foo""
5-10: ""bar""", diagram, ignoreLineEndingDifferences: true);
    }
}
