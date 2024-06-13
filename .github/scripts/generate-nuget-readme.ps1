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

# Add the <PackageReadmeFile>NUGET_README.md</PackageReadmeFile> to the csproj
$csproj = [xml](Get-Content -Path "./FoggyBalrog.MermaidDotNet/FoggyBalrog.MermaidDotNet.csproj")
$packageReadmeFile = $csproj.Project.PropertyGroup.PackageReadmeFile
if ($null -eq $packageReadmeFile) {
    $packageReadmeFile = $csproj.CreateElement("PackageReadmeFile")
    $csproj.Project.PropertyGroup.AppendChild($packageReadmeFile)
}
$packageReadmeFile.InnerText = "NUGET_README.md"

# Include the NUGET_README.md in the csproj
$itemGroup = $csproj.Project.ItemGroup
$none = $csproj.CreateElement("None")
$none.SetAttribute("Include", "..\NUGET_README.md")
$pack = $csproj.CreateElement("Pack")
$pack.InnerText = "True"
$none.AppendChild($pack)
$packagePath = $csproj.CreateElement("PackagePath")
$packagePath.InnerText = "\"
$none.AppendChild($packagePath)
$itemGroup.AppendChild($none)

# Save the csproj file
$csproj.Save("./FoggyBalrog.MermaidDotNet/FoggyBalrog.MermaidDotNet.csproj")