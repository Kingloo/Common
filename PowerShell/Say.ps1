function Say(
	[parameter(Mandatory=$true)]
	[string]
	$Text)
{
	Add-Type -AssemblyName System.Speech;
	$voice = New-Object System.Speech.Synthesis.SpeechSynthesizer;
	$voice.SelectVoice("Microsoft Zira Desktop");
	$voice.Rate = -1;
	$voice.Speak($Text);
}