function Encrypt(
    [parameter(Mandatory=$true, ValueFromPipeline=$true)]
    [string]
    $File,
    [parameter(Mandatory=$true)]
    [string]
    $Password,
    [parameter(Mandatory=$false)]
    [switch]
    $DeleteOriginal=$false
)
{
    $aescrypt = "C:\Users\k1ngl\Utils\aescrypt.exe";
    $minPasswordLength = 12;

    if (-Not (Test-Path $aescrypt))
    {
        Write-Error "aescrypt not found at $($aescrypt)";
        throw;
    }

    if (-Not (Test-Path $File))
    {
        Write-Error "$($File) does not exist!"
        throw;
    }

    if ($Password.Length -lt $minPasswordLength)
    {
        Write-Error "password must be at least $($minPasswordLength) characters long, yours was $($Password.Length)";
        throw;
    }

    $encryptedFile = $File + ".aes";

    $output = & $aescrypt -e -p $Password -o $encryptedFile $File;

    if ($LASTEXITCODE -ne 0)
    {
        Write-Error "creating encrypted archive failed";
        throw;
    }
    
    if ($DeleteOriginal -eq $true)
    {
        Write-Host "deleting unencrypted: $($File)";
        Remove-Item -Path $File;
    }

    return $encryptedFile;
}