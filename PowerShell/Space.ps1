function Space()
{
	$format = "#.#";
	$gib = 1024 * 1024 * 1024;

	$totalUsed = 0;
	$totalFree = 0;
	$totalSize = 0;

	Write-Host "";

	Write-Host "Space on Mounted Shares (used, free, total) (GiBs)";

	Write-Host "";

	$drives = Get-WmiObject -Class Win32_logicaldisk | Where-Object { $_.DriveType -eq 4 };

	foreach ($drive in $drives)
	{
		$free = $drive.FreeSpace;
		$used = $drive.Size - $drive.FreeSpace;
		$size = $drive.Size;

		$totalUsed += $used;
		$totalFree += $free;
		$totalSize += $size;

		$hrUsed = ($used / $gib).ToString($format);
		$hrFree = ($free / $gib).ToString($format);
		$hrSize = ($size / $gib).ToString($format);
	
		Write-Host "$($drive.DeviceID)`t`t$($hrUsed)`t`t$($hrFree)`t`t$($hrSize)`t`t$($drive.VolumeName)`t`t$($drive.ProviderName)";
	}

	Write-Host "";

	$hrTotalUsed = ($totalUsed / $gib).ToString($format);
	$hrTotalFree = ($totalFree / $gib).ToString($format);
	$hrTotalSize = ($totalSize / $gib).ToString($format);

	# https://stackoverflow.com/questions/21838419/create-table-with-variables-in-powershell

	Write-Host "Total`t`t$($hrTotalUsed)`t`t$($hrTotalFree)`t`t$($hrTotalSize)";

	Write-Host "";
}
