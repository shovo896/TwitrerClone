$ErrorActionPreference = 'SilentlyContinue'
$before = (Get-CimInstance Win32_LogicalDisk -Filter "DeviceID='C:'").FreeSpace

$targets = @(
    'C:\Users\Shuvo\AppData\Local\Microsoft\vscode-cpptools\ipch',
    'C:\Users\Shuvo\AppData\Local\Microsoft\Edge\User Data\Default\Service Worker\CacheStorage',
    'C:\Users\Shuvo\AppData\Local\Microsoft\Edge\User Data\Default\Service Worker\ScriptCache',
    'C:\Users\Shuvo\AppData\Local\Microsoft\Edge\User Data\Default\Code Cache',
    'C:\Users\Shuvo\AppData\Local\Microsoft\Edge\User Data\Default\GPUCache',
    'C:\Users\Shuvo\AppData\Local\Microsoft\Edge\User Data\Default\DawnWebGPUCache',
    'C:\Users\Shuvo\AppData\Local\Microsoft\Edge\User Data\Crashpad',
    'C:\Users\Shuvo\AppData\Roaming\Code\CachedExtensionVSIXs',
    'C:\Users\Shuvo\AppData\Roaming\Code\Crashpad',
    'C:\Users\Shuvo\AppData\Roaming\Code\logs',
    'C:\Users\Shuvo\.codex\cache',
    'C:\Users\Shuvo\.codex\.tmp'
)

$allowedPrefixes = @(
    'C:\Users\Shuvo\AppData\Local\Microsoft\',
    'C:\Users\Shuvo\AppData\Roaming\Code\',
    'C:\Users\Shuvo\.codex\'
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

$after = (Get-CimInstance Win32_LogicalDisk -Filter "DeviceID='C:'").FreeSpace
[pscustomobject]@{
    BeforeGB           = [math]::Round($before / 1GB, 2)
    AfterGB            = [math]::Round($after / 1GB, 2)
    ReclaimedGB        = [math]::Round(($after - $before) / 1GB, 2)
    StillNeededFor40GB = [math]::Max(0, [math]::Round(40 - $after / 1GB, 2))
} | Format-List
