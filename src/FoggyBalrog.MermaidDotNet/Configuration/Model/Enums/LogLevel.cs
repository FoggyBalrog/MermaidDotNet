using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum LogLevel
{
    [EnumMember(Value = "trace")]
    Trace = 0,

    [EnumMember(Value = "debug")]
    Debug = 1,

    [EnumMember(Value = "info")]
    Info = 2,

    [EnumMember(Value = "warn")]
    Warn = 3,

    [EnumMember(Value = "error")]
    Error = 4,

    [EnumMember(Value = "fatal")]
    Fatal = 5
}
