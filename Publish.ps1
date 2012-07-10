param($argsfile = "")

$ClickOnceName = "MetroFire"
$CloneUrl = "git://github.com/ArildF/MetroFire.git"
$ProjectFile = "src\MetroFire\MetroFire.csproj"
$MsBuild = "$env:SystemRoot\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
$PublishUrl = "http://metrofire.s3.amazonaws.com/"
$Bukkit ="metrofire"

if($argsfile -ne "" -and (Test-Path $argsfile))
{
    . $argsfile
}

Write-Host "$ClickOnceName.application"

$dir = [Guid]::NewGuid().ToString()
$dir = Join-Path $env:temp $dir

$publishDir = [Guid]::NewGuid().ToString() + "\"
$publishDir = Join-Path $env:temp $publishDir

git clone --depth 1 $CloneUrl $dir

pushd
cd $dir



& $MsBuild $ProjectFile /p:Configuration=Release `
    /t:Publish `
    /p:PublishDir=$publishDir `
    /p:PublishUrl=$PublishUrl `
    /p:InstallUrl=$PublishUrl `
    /p:ProductName=$ClickOnceName `
    /p:ApplicationName=$ClickOnceName `
    /p:AssemblyNameOverride=$ClickOnceName `
    /p:TargetDeployManifestFilename=$ClickOnceName.application 

popd

S3Uploader $publishDir $Bukkit

Write-Host $publishDir

Remove-Item $dir -re -fo
Remove-Item $publishDir -re -fo


