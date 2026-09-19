namespace FoggyBalrog.MermaidDotNet.UnitTests;

internal static class CompatibilityTestHelpers
{
    internal static IEnumerable<(MermaidDotNetOptions Options, bool Allowed)> Cases(int minor)
    {
        foreach (bool validateInputs in new[] { false, true })
        {
            foreach (bool sanitizeInputs in new[] { false, true })
            {
                foreach (var target in new[] { new MermaidVersion(11, minor - 1, 99), new MermaidVersion(11, minor), new MermaidVersion(11, minor, 1), new MermaidVersion(12, 0) })
                {
                    yield return (new() { TargetMermaidVersion = target, ValidateInputs = validateInputs, SanitizeInputs = sanitizeInputs }, target >= new MermaidVersion(11, minor));
                }
                yield return (new() { TargetMermaidVersion = MermaidVersion.V11_0, ValidateInputs = validateInputs, SanitizeInputs = sanitizeInputs, CompatibilityMode = MermaidCompatibilityMode.Unchecked }, true);
            }
        }
    }

    internal static void AssertIncompatible(Action action)
    {
        var exception = Assert.Throws<MermaidException>(action);
        Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
    }
}
