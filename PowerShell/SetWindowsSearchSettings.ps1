function SetWindowsSearchSettings()
{
	Set-WindowsSearchSetting -EnableWebResultsSetting False -EnableMeteredWebResultsSetting False -SearchExperienceSetting NotPersonalized -SafeSearchSetting Strict;
}