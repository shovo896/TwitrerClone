$ErrorActionPreference = 'SilentlyContinue'

$before = (Get-CimInstance Win32_LogicalDisk -Filter "DeviceID='C:'").FreeSpace
$cutoff = (Get-Date).AddDays(-7)
$tempPath = 'C:\Users\Shuvo\AppData\Local\Temp'

Get-ChildItem -LiteralPath $tempPath -Force -File -Recurse -ErrorAction SilentlyContinue |
    Where-Object LastWriteTime -lt $cutoff |
    Remove-Item -Force -ErrorAction SilentlyContinue

Get-ChildItem -LiteralPath $tempPath -Force -Directory -Recurse -ErrorAction SilentlyContinue |
    Where-Object LastWriteTime -lt $cutoff |
    Sort-Object FullName -Descending |
    Remove-Item -Recurse -Force -ErrorAction SilentlyContinue

$targets = @(
    'C:\Users\Shuvo\AppData\Local\npm-cache',
    'C:\Users\Shuvo\AppData\Local\pip\Cache',
    'C:\Users\Shuvo\AppData\Local\NuGet\v3-cache',
    'C:\Users\Shuvo\AppData\Local\CrashDumps',
    'C:\Users\Shuvo\AppData\Local\Microsoft\Edge\User Data\Default\Cache',
    'C:\Users\Shuvo\AppData\Local\Google\Chrome\User Data\Default\Cache',
    'C:\Users\Shuvo\AppData\Roaming\Code\Cache',
    'C:\Users\Shuvo\AppData\Roaming\Code\CachedData',
    'C:\Users\Shuvo\AppData\Roaming\Code\CachedExtensions',
    'C:\Users\Shuvo\AppData\Local\JetBrains\Rider2026.2\caches',
    'C:\Users\Shuvo\AppData\Local\JetBrains\Rider2026.2\index',
    'C:\Users\Shuvo\AppData\Local\JetBrains\Rider2026.2\log',
    'C:\Users\Shuvo\AppData\Local\JetBrains\Rider2026.2\full-line',
    'C:\Users\Shuvo\AppData\Local\JetBrains\Daemon',
    'C:\Windows\SoftwareDistribution\Download'
)

$allowedPrefixes = @(
    'C:\Users\Shuvo\AppData\Local\',
    'C:\Users\Shuvo\AppData\Roaming\Code\',
    'C:\Windows\SoftwareDistribution\Download'
)

foreach ($target in $targets) {
    $fullPath = [IO.Path]::GetFullPath($target)
    $isAllowed = $false
    foreach ($prefix in $allowedPrefixes) {
        if ($fullPath.StartsWith($prefix, [StringComparison]::OrdinalIgnoreCase)) {
            $isAllowed = $true
            break
        }
    }
    if ($isAllowed -and (Test-Path -LiteralPath $fullPath)) {
        Get-ChildItem -LiteralPath $fullPath -Force -ErrorAction SilentlyContinue |
            Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    }
}

Clear-RecycleBin -DriveLetter C -Force -ErrorAction SilentlyContinue

$after = (Get-CimInstance Win32_LogicalDisk -Filter "DeviceID='C:'").FreeSpace
[pscustomobject]@{
    BeforeGB           = [math]::Round($before / 1GB, 2)
    AfterGB            = [math]::Round($after / 1GB, 2)
    ReclaimedGB        = [math]::Round(($after - $before) / 1GB, 2)
    StillNeededFor40GB = [math]::Max(0, [math]::Round(40 - $after / 1GB, 2))
} | Format-List
