namespace FoggyBalrog.MermaidDotNet.PacketDiagram.Model;

internal abstract record AbstractField(string? Description);

internal record EndField(int End, string? Description) : AbstractField(Description);

internal record BitsFiels(int Bits, string? Description) : AbstractField(Description);