function ClearDotnetSingleFileCache()
{
	$dotnetCacheFolder = $env:UserProfile + "\AppData\Local\Temp\.net";

	if (Test-Path $dotnetCacheFolder)
	{
		Remove-Item -Recurse -Path ($dotnetCacheFolder + "\*");
	}
}