using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record GitGraphDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "titleTopMargin")]
    public int? TitleTopMargin { get; set; }

    [YamlMember(Alias = "diagramPadding")]
    public int? DiagramPadding { get; set; }

    [YamlMember(Alias = "nodeLabel")]
    public NodeLabel? NodeLabel { get; set; }

    [YamlMember(Alias = "mainBranchName")]
    public string? MainBranchName { get; set; }

    [YamlMember(Alias = "mainBranchOrder")]
    public int? MainBranchOrder { get; set; }

    [YamlMember(Alias = "showCommitLabel")]
    public bool? ShowCommitLabel { get; set; }

    [YamlMember(Alias = "showBranches")]
    public bool? ShowBranches { get; set; }

    [YamlMember(Alias = "rotateCommitLabel")]
    public bool? RotateCommitLabel { get; set; }

    [YamlMember(Alias = "parallelCommits")]
    public bool? ParallelCommits { get; set; }

    [YamlMember(Alias = "arrowMarkerAbsolute")]
    public bool? ArrowMarkerAbsolute { get; set; }
}
