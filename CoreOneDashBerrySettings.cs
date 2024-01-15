namespace Celeste.Mod.CoreOneDashBerry;

public class CoreOneDashBerrySettings : EverestModuleSettings {
    [SettingSubText("Shows an indicator above Madeline if the second dash was used.\nEntering a screen transition makes this state permanent and the berry won't spawn.")]
    public bool ShowFailIndicator { get; set; } = true;
    [SettingSubText("Kills the player when the second dash is used.")]
    public bool KillOnFail { get; set; } = false;
}