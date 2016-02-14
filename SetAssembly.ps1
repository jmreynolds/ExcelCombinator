$output = & "GitVersion.exe"
$joined = $output -join "`n"
$versionInfo = $joined | ConvertFrom-Json
$version = $versionInfo.MajorMinorPatch



Write-Output $version