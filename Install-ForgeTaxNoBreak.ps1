[CmdletBinding()]
param(
    [string]$GamePath
)

$ErrorActionPreference = 'Stop'

function Resolve-GamePath {
    param([string]$Candidate)

    if ($Candidate) {
        return (Resolve-Path -LiteralPath $Candidate).Path
    }

    $packageDirectory = (Resolve-Path -LiteralPath $PSScriptRoot).Path
    if (Test-Path -LiteralPath (Join-Path $packageDirectory 'ForgeTax.exe') -PathType Leaf) {
        return $packageDirectory
    }

    $enteredPath = Read-Host 'Enter the ForgeTax game folder containing ForgeTax.exe'
    if (-not $enteredPath) {
        throw 'No game folder was provided.'
    }
    return (Resolve-Path -LiteralPath $enteredPath).Path
}

$resolvedGamePath = Resolve-GamePath -Candidate $GamePath
$gameExe = Join-Path $resolvedGamePath 'ForgeTax.exe'
if (-not (Test-Path -LiteralPath $gameExe -PathType Leaf)) {
    throw "ForgeTax.exe was not found in '$resolvedGamePath'."
}

$payloadPath = Join-Path $PSScriptRoot 'payload'
if (-not (Test-Path -LiteralPath $payloadPath -PathType Container)) {
    throw "The package payload was not found at '$payloadPath'."
}

$bootstrapFiles = @('winhttp.dll', 'doorstop_config.ini', '.doorstop_version')
$existingLoader = (Test-Path -LiteralPath (Join-Path $resolvedGamePath 'BepInEx\core\BepInEx.dll')) -or
    (Test-Path -LiteralPath (Join-Path $resolvedGamePath 'winhttp.dll'))

if ($existingLoader) {
    Write-Host 'Existing BepInEx bootstrap detected; preserving its loader files.'
} else {
    foreach ($fileName in $bootstrapFiles) {
        $source = Join-Path $payloadPath $fileName
        if (Test-Path -LiteralPath $source) {
            Copy-Item -LiteralPath $source -Destination (Join-Path $resolvedGamePath $fileName) -Force
        }
    }
}

$sourceCore = Join-Path $payloadPath 'BepInEx\core'
$destinationCore = Join-Path $resolvedGamePath 'BepInEx\core'
$destinationPlugins = Join-Path $resolvedGamePath 'BepInEx\plugins'
New-Item -ItemType Directory -Force -Path $destinationCore, $destinationPlugins | Out-Null

if (-not $existingLoader) {
    Copy-Item -Path (Join-Path $sourceCore '*') -Destination $destinationCore -Force
}

Copy-Item -LiteralPath (Join-Path $payloadPath 'BepInEx\plugins\ForgeTaxNoBreak.dll') -Destination $destinationPlugins -Force

Write-Host ''
Write-Host 'ForgeTax No Break was installed.' -ForegroundColor Green
Write-Host "Plugin: $(Join-Path $destinationPlugins 'ForgeTaxNoBreak.dll')"
Write-Host 'Start the game through Steam. The plugin is enabled by default.'

