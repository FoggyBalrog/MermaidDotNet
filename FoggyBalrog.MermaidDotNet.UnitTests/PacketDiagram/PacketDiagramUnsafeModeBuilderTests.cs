namespace FoggyBalrog.MermaidDotNet.UnitTests.PacketDiagram;

public class PacketDiagramUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .Unsafe
            .PacketDiagram()
            .Build();

        Assert.Equal("packet", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithEndFieldFirst()
    {
        string diagram = Mermaid
            .Unsafe
            .PacketDiagram("some title")
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
            .Unsafe
            .PacketDiagram("some title")
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