using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum Look
{
    [EnumMember(Value = "classic")]
    Classic,

    [EnumMember(Value = "handDrawn")]
    HandDrawn
}
