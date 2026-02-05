# Packet Diagram<!-- omit from toc -->

*Official Mermaid documentation: [Packet Diagram](https://mermaid.js.org/syntax/packet.html).*

> [!NOTE]
> All Mermaid diagrams can be configured, by passing a `MermaidConfig` object to any of the methods in the `Mermaid` class. Read more on [Mermaid configuration](~/configuration.md).

## Simple packet diagram

The following code sample shows how to create a simple Mermaid packet diagram.

Use the `PacketDiagram` method of the `Mermaid` class to create a packet diagram. You ca provide an optional `title` argument.

Add fields with the `AddFieldWithEnd` (where you specify the end bit) or `AddFieldWithBits` (where you specify the bits length) methods.

Generate the diagram mermaid code with the `Build` method.

```csharp
string diagram = Mermaid
    .PacketDiagram("some title")
    .AddFieldWithEnd(10, "foo")
    .AddFieldWithBits(5, "bar")
    .AddFieldWithEnd(25, "baz")
    .Build();
```

The code above generates the following Mermaid code:

```text
---
title: some title
---
packet
0-10: "foo"
+5: "bar"
16-25: "baz"
```

That renders as:

```mermaid
---
title: some title
---
packet
0-10: "foo"
+5: "bar"
16-25: "baz"
```

[⬆ Back to top](#packet-diagram)