$ErrorActionPreference = 'SilentlyContinue'

$targets = @(
    'C:\Users\Shuvo\.codex\sessions',
    'C:\Users\Shuvo\AppData\Roaming\Python',
    'C:\Users\Shuvo\.vscode\extensions',
    'C:\Users\Shuvo\AppData\Local\JetBrains\Rider2026.2\acp-agents',
    'C:\Users\Shuvo\AppData\Local\Programs',
    'C:\Users\Shuvo\AppData\Local\Packages\PythonSoftwareFoundation.Python.3.13_qbz5n2kfra8p0',
    'C:\Users\Shuvo\AppData\Roaming\npm',
    'C:\Users\Shuvo\.nuget',
    'C:\Users\Shuvo\AppData\Roaming\Code\User',
    'C:\Users\Shuvo\AppData\Roaming\Code\WebStorage',
    'C:\Program Files\MATLAB\R2026a'
)

$rows = @()
foreach ($target in $targets) {
    $freeBefore = (Get-CimInstance Win32_LogicalDisk -Filter "DeviceID='C:'").FreeSpace
    if (($freeBefore / 1GB) -ge 40) {
        break
    }
    if (-not (Test-Path -LiteralPath $target)) {
        continue
    }

    Write-Output ("Compressing: {0}" -f $target)
    & compact.exe /c "/s:$target" /i /q /exe:lzx | Out-Null

    $freeAfter = (Get-CimInstance Win32_LogicalDisk -Filter "DeviceID='C:'").FreeSpace
    $row = [pscustomobject]@{
        Path       = $target
        GainedGB   = [math]::Round(($freeAfter - $freeBefore) / 1GB, 2)
        FreeNowGB  = [math]::Round($freeAfter / 1GB, 2)
    }
    $rows += $row
    $row | Format-List
}

$finalFree = (Get-CimInstance Win32_LogicalDisk -Filter "DeviceID='C:'").FreeSpace
[pscustomobject]@{
    FinalFreeGB = [math]::Round($finalFree / 1GB, 2)
    TargetMet   = (($finalFree / 1GB) -ge 40)
} | Format-List
