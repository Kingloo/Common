function HashCmp(
	[parameter(Mandatory=$true)]
	[string]
	$File1,
	[parameter(Mandatory=$true)]
	[string]
	$File2,
	[parameter(Mandatory=$false)]
	[string]
	$Algorithm = "SHA256")
{
	$file1Hash = (Get-FileHash -Algorithm $algorithm $file1).Hash;
	$file2Hash = (Get-FileHash -Algorithm $algorithm $file2).Hash;

	Write-Host($algorithm + " of " + $file1 + " = " + $file1Hash);
	Write-Host($algorithm + " of " + $file2 + " = " + $file2Hash);

	if ($file1Hash.Equals($file2Hash))
	{
		Write-Host("hashes are EQUAL");
	}
	else
	{
		Write-Host("hashes are NOT equal");
	}
}