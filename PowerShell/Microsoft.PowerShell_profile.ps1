Set-StrictMode -Version 2

$includeDirectory = "\Common\PowerShell\*";

$psFiles = Get-ChildItem -Path $includeDirectory -Include "*.ps1";

foreach ($psFile in $psFiles)
{
    try {
        . ($psFile);
    }
    catch {
        Write-Host "error loading $($psFile)";
    }
}