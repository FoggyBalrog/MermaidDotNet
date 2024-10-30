﻿using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum MessageAlign
{
    [EnumMember(Value = "left")]
    Left,

    [EnumMember(Value = "center")]
    Center,

    [EnumMember(Value = "right")]
    Right
}
