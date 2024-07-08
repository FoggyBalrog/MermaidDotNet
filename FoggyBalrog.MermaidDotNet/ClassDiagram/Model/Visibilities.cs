namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

/// <summary>
/// The possible visibilities of a class member. Can be combined, e.g. <c>Visibilities.Public | Visibilities.Static</c>.
/// </summary>
[Flags]
public enum Visibilities
{
    None = 0,
    Public = 1 << 0,
    Private = 1 << 1,
    Protected = 1 << 2,
    Internal = 1 << 3,
    Abstract = 1 << 4,
    Static = 1 << 5
}
