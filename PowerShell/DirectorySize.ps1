function DirectorySize(
	[parameter(Mandatory=$true, ValueFromPipeline=$true)]
	[string]
	$Directory,
	[parameter(Mandatory=$false, ValueFromPipeline=$false)]
	[int]
	$GreaterThan = 10
)
{
	$format = "#.##";
	$gib = 1024 * 1024 * 1024;

	$all = New-Object 'System.Collections.Generic.Dictionary[string,double]';

	foreach ($dir in Get-ChildItem -Directory $Directory)
	{
		$size = (Get-ChildItem -File -Recurse -Path $dir.FullName | ForEach-Object { $_.Length } | Measure-Object -Sum).Sum;
		$hrSize = $size / $gib;

		if ($hrSize -gt $GreaterThan)
		{
			$all[$dir.FullName] = $hrSize;
		}
	}

	$all.GetEnumerator() |
		Sort-Object -Descending { $_.Value } |
		Format-Table @{
			Label      = 'Directory';
			Expression = { $_.Key };
		},
		@{
			Label      = 'Size (GiBs)';
			Expression = { $_.Value.ToString($format) };
		}
}