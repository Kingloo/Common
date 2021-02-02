function BlockWindowsSearchFirewall()
{
	Get-NetFirewallRule -DisplayName "Windows Search" | Set-NetFirewallRule -Action Block;
}