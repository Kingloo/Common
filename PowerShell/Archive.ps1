function Archive(
    [parameter(Mandatory=$true, ValueFromPipeline=$true)]
    [string]
    $FileList,    
    [parameter(Mandatory=$true)]
    [string]
    $Path
)
{
    $7zip = $Env:ProgramFiles + "\7-Zip\7z.exe";
    $requiredDirectory = "C:\Users\";

    if (-Not (Test-Path $7zip))
    {
        Write-Error "7-Zip not found at $($7zip)";
        throw;
    }

    if (-Not ((Get-Item -Path $FileList).FullName.StartsWith($requiredDirectory)))
    {
        Write-Error "this script only operates on files or globs within the $($requiredDirectory) directory";
        throw;
    }

    $output = & $7zip a -t7z -mx0 -bb0 -r $Path $FileList;

    if ($LASTEXITCODE -ne 0)
    {
        Write-Error "archive creation failed!";
        throw;
    }

    return (Get-Item -Path $Path).FullName;
}