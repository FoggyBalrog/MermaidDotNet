# Get the tag
$tag = git describe --exact-match --tags HEAD

if ($null -eq $tag -or $tag -eq '') {
    Write-Host "##[error] Could not find tag"
    exit 1
}

# Get the version from the tag
$tagVersion = $tag -replace 'v', ''

if ($null -eq $tagVersion -or $tagVersion -eq '') {
    Write-Host "##[error] Could not determine version from tag"
    exit 1
}

# Get the version from the csproj
$csprojVersion = ([xml](Get-Content -Path "./FoggyBalrog.MermaidDotNet/FoggyBalrog.MermaidDotNet.csproj")).Project.PropertyGroup.Version

# Compare the versions and fail if they do not match
if ($tagVersion -ne $csprojVersion) {
    Write-Host "##[error] NuGet version in csproj ($csprojVersion) does not match the version tag ($tagVersion)"
    exit 1
}