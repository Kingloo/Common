function S(
	[parameter(Mandatory=$true)]
	[string]
	$Stream,
	[parameter(Mandatory=$false)]
	[string]
	$Quality = "best")
{
	streamlink.exe $stream $quality;
}