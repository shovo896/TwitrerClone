$taskbarPath = 'C:\Users\Shuvo\AppData\Roaming\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar'
$targets = @(
    'C:\Users\Shuvo\Desktop\Linkedin.lnk',
    'C:\Users\Shuvo\Desktop\WhatsApp.lnk'
)

if (Test-Path -LiteralPath $taskbarPath) {
    $taskbarMatches = Get-ChildItem -LiteralPath $taskbarPath -Force -File -ErrorAction SilentlyContinue |
        Where-Object { $_.Name -match '^(WhatsApp|LinkedIn)\.lnk$' }
    foreach ($match in $taskbarMatches) {
        $targets += $match.FullName
    }
}

foreach ($target in ($targets | Select-Object -Unique)) {
    if (Test-Path -LiteralPath $target) {
        Remove-Item -LiteralPath $target -Force
    }
}

[pscustomobject]@{
    LinkedInDesktopExists  = Test-Path -LiteralPath 'C:\Users\Shuvo\Desktop\Linkedin.lnk'
    WhatsAppDesktopExists  = Test-Path -LiteralPath 'C:\Users\Shuvo\Desktop\WhatsApp.lnk'
    TaskbarMatchesRemaining = @(
        Get-ChildItem -LiteralPath $taskbarPath -Force -File -ErrorAction SilentlyContinue |
            Where-Object { $_.Name -match '^(WhatsApp|LinkedIn)\.lnk$' }
    ).Count
} | Format-List
