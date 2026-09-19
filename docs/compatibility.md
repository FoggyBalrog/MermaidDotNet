# Mermaid version compatibility

The default target is Mermaid **11.4**, with `MermaidCompatibilityMode.Strict`. Strict checks are independent of `ValidateInputs` and `SanitizeInputs`. Use `TargetMermaidVersion` to select the renderer version you actually deploy, or `MermaidCompatibilityMode.Unchecked` for unrestricted generation within the library's 11.0+ support range.

`MermaidCompatibility.LatestTestedVersion` is **11.13**. It records parser integration-test coverage, not a ceiling on strict targets. Strict mode applies known feature boundaries to newer targets, but cannot guarantee compatibility with untested releases. No mode rewrites syntax to match a target.

## Feature matrix

Minimum versions are inclusive; removal versions are exclusive. The registry describes the syntax emitted by this library, within its supported 11.0+ range. A diagram's historical introduction may be earlier than the minimum for its emitted header.

| Implemented feature | Minimum | Unsupported from | Check |
| --- | --- | --- | --- |
| Flowchart, sequence, class, state, ER, user journey, Gantt, Git graph, mind map, pie, quadrant, requirement, timeline diagrams | 11.0 | — | `Build()` |
| Kanban diagrams | 11.4 | — | `Build()` |
| Packet diagrams (`packet` header) | 11.9 | — | `Build()` |
| Block, Sankey, XY diagrams (`block`, `sankey`, `xychart` headers) | 11.10 | — | `Build()` |
| Expanded flowchart shapes, including familiar shapes emitted as `@{ shape: ... }` | 11.3 | — | `AddNodeWithExpandedShape()` |
| Flowchart per-edge curves, every `CurveStyle` value | 11.10 | — | `AddLink()` / `AddLinkChain()` when `curveStyle` is set |
| Gantt vertical markers | 11.7 | — | `AddVerticalMarker()` |
| Packet bits syntax | 11.7 | — | `AddFieldWithBits()`; the emitted diagram header still requires 11.9 |
| State hyperlinks, with or without tooltip | 11.7 | — | `AddStateLink()` |
| Sequence `Boundary`, `Control`, `Entity`, `Database`, `Collections`, `Queue` members with aliases | 11.13 | — | `AddMember()` / `SendCreateMessage()` |
| Dotted class namespace names | 11.3 | — | `AddNamespace()` |
| Nested class namespace blocks | 11.15 | — | Nested `AddNamespace()` calls |
| ER multiline relationship labels (`<br/>` or `<br />`, including sanitized newlines) | 11.1 | — | `AddRelationship()` |
| `Elk.CycleBreakingStrategy`, every enum value | 11.1 | — | `Build()` |
| `Kanban` configuration section | 11.4 | — | `Build()` |
| `Journey.TitleColor`, `TitleFontFamily`, `TitleFontSize` | 11.7 | — | `Build()` |
| `XYChart.ShowDataLabel`, including `false` | 11.7 | — | `Build()` |
| `Flowchart.DefaultRenderer` | 11.0 | 12.0 | `Build()` |
| `Class.DefaultRenderer` | 11.0 | 12.0 | `Build()` |
| `State.DefaultRenderer` | 11.0 | 12.0 | `Build()` |

Legacy flowchart shapes and ordinary sequence `Participant` / `Actor` values remain available at the baseline. Sequence participant shapes first appeared in 11.11, but this library emits their metadata together with an external alias, requiring 11.13.

All 18 top-level builders check the entire supplied typed configuration, even sections for other diagram types. Checks inspect property presence, not truthiness: `false`, zero, and a default enum value still get serialized. Configuration remains mutable and is checked again on every `Build()`; input options are snapshotted at builder creation.

## Resolving failures

Failures throw `MermaidException` with reason `IncompatibleVersion`. The message identifies the feature, target, allowed range, and remediation. Select a target in the allowed range, remove the incompatible feature, or use an older equivalent where available:

- Use `AddNode()` with a legacy `NodeShape` instead of expanded flowchart shape syntax.
- Omit per-edge `curveStyle`, or use diagram-wide `Flowchart.Curve` configuration.
- Use ordinary participants/actors instead of newer sequence member shapes.
- Use flat class namespaces instead of nested blocks.

For Mermaid 12, remove **all** flowchart, class, and state `DefaultRenderer` settings. Use the top-level `Layout` property instead:

```csharp
var options = new MermaidDotNetOptions
{
    TargetMermaidVersion = MermaidVersion.V12_0
};
var config = new MermaidConfig
{
    Layout = "elk" // or "dagre"
};

string diagram = Mermaid.Flowchart(config: config, options: options)
    .AddNode("Start", out _)
    .Build();
```

`Unchecked` skips feature checks, including removal checks, but does not make unsupported output valid. It also does not disable ordinary input validation or sanitization.

## Audit scope and evidence

The audit covers all implemented diagram types, builder operations, enum/shape values, emitted directives, and typed configuration properties. Other implemented syntax is available at the 11.0 baseline unless listed above. Deprecation alone is not removal: for example, `Flowchart.HtmlLabels` is deprecated in 11.13 but remains supported in Mermaid 12.

Compatibility checking is not a parser for arbitrary strings or extension data. It does not interpret custom CSS, theme-variable dictionaries, arbitrary layout names, caller-written ER attribute syntax, or Markdown-looking ordinary text. Upstream bug fixes and rendering differences are not automatically treated as feature introductions. Sequence half-arrows are not currently exposed by this library and therefore have no registry entry.

The registry lives in `MermaidFeatureRegistry.cs`, keyed by the internal `MermaidFeature` identifiers. Builders use the internal `EnsureCompatible` extension methods on their options, implemented centrally in `MermaidCompatibility`, and never compare version literals. `EnsureSupportedTarget` separately checks the options when they are snapshotted. Add new version-sensitive APIs to the registry and wire their checks and boundary tests together.

Boundary evidence comes from the tagged upstream [changelog](https://github.com/mermaid-js/mermaid/blob/mermaid%4012.0.0/packages/mermaid/CHANGELOG.md) and configuration schemas:

- **11.1:** multiline ER labels (PR #5711) and `elk.cycleBreakingStrategy` ([schema](https://github.com/mermaid-js/mermaid/blob/mermaid%4011.1.0/packages/mermaid/src/schemas/config.schema.yaml)).
- **11.3:** [expanded flowchart shapes](https://github.com/mermaid-js/mermaid/blob/mermaid%4011.3.0/packages/mermaid/src/docs/syntax/flowchart.md) and dotted class namespaces (#5849).
- **11.4:** Kanban diagrams and configuration (#5999).
- **11.7:** packet relative fields (#5980), state hyperlinks (#6423), Gantt vertical markers (#6479), journey title styling (#6225), XY data labels (#6475).
- **11.9 / 11.10:** non-`beta` headers (#6510 / #6653); per-edge curves in 11.10 (#6744).
- **11.13:** aliases with sequence participant metadata (#7136).
- **11.15:** [nested class namespaces](https://github.com/mermaid-js/mermaid/blob/mermaid%4011.15.0/packages/mermaid/src/docs/syntax/classDiagram.md).
- **12.0:** removal of diagram-level `defaultRenderer` in favor of top-level `layout` (#8211).

Unit tests exercise exact introduction/removal boundaries, unchecked bypass, mutable configuration, and rejected operations without state changes. Parser integration tests run against 11.13; they are not a rendering matrix for every listed release.
