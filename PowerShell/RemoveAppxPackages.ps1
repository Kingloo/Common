function RemoveAppxPackages()
{
    $names =
        "zunemu",
        "zunevi",
        "officehu",
        "onenote",
        "getstart",
        "3dbuilder",
        "maps",
        "bingsports",
        "bingfinance",
        "bingweather",
        "bingnews",
        "microsoftsolitaire",
        "sway",
        "skypeapp",
        "messaging",
        "commsphone",
        "windowsphone",
        "windowscommunicationsapps",
        "photos",
        "connectivitystore",
        "3d",
        "mixed",
        "sticky",
        "wallet",
        "windowsfeedback",
        "people",
        "camera",
        "phone",
        "help",
        "spotify",
        "candy"

    foreach ($name in $names)
    {
        Get-AppxPackage *$name* | Remove-AppxPackage
    }
}