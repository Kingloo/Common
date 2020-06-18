function Uptime()
{
	$uptime = (Get-Date) - (Get-CimInstance Win32_OperatingSystem).LastBootUpTime;
	Write-Host "$($uptime.Days) days $($uptime.Hours) hours $($uptime.Minutes) minutes $($uptime.Seconds) seconds";
}