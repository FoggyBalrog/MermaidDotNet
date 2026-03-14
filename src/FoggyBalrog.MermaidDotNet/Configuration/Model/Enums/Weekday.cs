using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum Weekday
{
    [EnumMember(Value = "monday")]
    Monday,

    [EnumMember(Value = "tuesday")]
    Tuesday,

    [EnumMember(Value = "wednesday")]
    Wednesday,

    [EnumMember(Value = "thursday")]
    Thursday,

    [EnumMember(Value = "friday")]
    Friday,

    [EnumMember(Value = "saturday")]
    Saturday,

    [EnumMember(Value = "sunday")]
    Sunday
}
