function BlockCortanaFirewall()
{
	Get-NetFirewallRule -DisplayName "Cortana" | Set-NetFirewallRule -Action Block;
}