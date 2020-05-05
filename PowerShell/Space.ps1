$format = "#.#"
$gib = 1024 * 1024 * 1024

$totalUsed = 0
$totalFree = 0

Write-Host ""

Write-Host "Space on Mounted Shares (used, free) (GiBs)"

Write-Host ""

$drives = Get-WmiObject -Class Win32_logicaldisk | Where-Object { $_.DriveType -eq 4 }

foreach ($drive in $drives)
{
	$free = $drive.FreeSpace
	$used = $drive.Size - $drive.FreeSpace

	$totalUsed += $used
	$totalFree += $free

	$hrUsed = ($used / $gib).ToString($format)
	$hrFree = ($free / $gib).ToString($format)
		
	Write-Host "$($drive.DeviceID)`t`t$($hrUsed)`t`t$($hrFree)`t`t$($drive.VolumeName)`t`t$($drive.ProviderName)"
}

Write-Host ""

$hrTotalUsed = ($totalUsed / $gib).ToString($format)
$hrTotalFree = ($totalFree / $gib).ToString($format)

Write-Host "Total`t`t$($hrTotalUsed)`t`t$($hrTotalFree)"
Write-Host ""