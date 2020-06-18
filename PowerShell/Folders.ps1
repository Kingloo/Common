function Folders()
{
	$format = "#.##";
	$gib = 1024 * 1024 * 1024;

	$all = New-Object 'System.Collections.Generic.Dictionary[string,double]';

	$drives = $drives = Get-WmiObject -Class Win32_logicaldisk | Where-Object { $_.DriveType -eq 4 };

	foreach ($drive in $drives)
	{
		$directories = Get-ChildItem -Directory $drive.DeviceID;

		foreach ($directory in $directories)
		{
			$size = (Get-ChildItem -File -Recurse -Path $directory.FullName | ForEach-Object { $_.Length } | Measure-Object -Sum).Sum;
			$hrSize = $size / $gib;

			if ($hrSize -gt 10)
			{
				$all[$directory.FullName] = $hrSize;
			}
		}
    }
    
    $all.GetEnumerator() |
		Sort-Object -Descending { $_.Value } |
		Format-Table @{
				Label = 'Directory';
				Expression = { $_.Key };
			},
			@{
				Label = 'Size (GiBs)';
				Expression = { $_.Value.ToString($format) };
			}
}