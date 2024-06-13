# Get the tag
$tag = git describe --exact-match --tags HEAD

if ($null -eq $tag -or $tag -eq '') {
    Write-Host "##[error] Could not find tag"
    exit 1
}

# Read the README.md file
$readmeContent = Get-Content -Path "./README.md" -Raw

# Replace the relative links with absolute links
$readmeContent = $readmeContent -replace '\(./docs', "(https://github.com/FoggyBalrog/MermaidDotNet/tree/$tag/docs"

# Write the result to NUGET_README.md
$readmeContent | Out-File -FilePath "./NUGET_README.md"

# Add the <PackageReadmeFile>NUGET_README.md</PackageReadmeFile> to the csproj file
$csproj = [xml](Get-Content -Path "./FoggyBalrog.MermaidDotNet/FoggyBalrog.MermaidDotNet.csproj")
$packageReadmeFile = $csproj.Project.PropertyGroup.PackageReadmeFile
if ($null -eq $packageReadmeFile) {
    $packageReadmeFile = $csproj.CreateElement("PackageReadmeFile")
    $csproj.Project.PropertyGroup.AppendChild($packageReadmeFile)
}
$packageReadmeFile.InnerText = "NUGET_README.md"
$csproj.Save("./FoggyBalrog.MermaidDotNet/FoggyBalrog.MermaidDotNet.csproj")