using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.CoreOneDashBerry;

public class CoreOneDashBerry : EverestModule {
    public static CoreOneDashBerry Instance;
    public override Type SessionType => typeof(CoreOneDashBerrySession);
    public static CoreOneDashBerrySession Session => (CoreOneDashBerrySession) Instance._Session;
    public override Type SettingsType => typeof(CoreOneDashBerrySettings);
    public static CoreOneDashBerrySettings Settings => (CoreOneDashBerrySettings) Instance._Settings;

    public CoreOneDashBerry() {
        Instance = this;
        Logger.SetLogLevel("CoreOneDashBerry", 0);
    }

    public override void Load() {
        On.Celeste.Player.StartDash += OneDashBerry.OnDash;
        On.Celeste.Player.Die += OneDashBerry.OnDeath;
        On.Celeste.Level.TransitionTo += OneDashBerry.OnTransition;
        On.Celeste.Player.Render += OneDashBerry.OnRender;
    }

    public override void Unload() {
        On.Celeste.Player.StartDash -= OneDashBerry.OnDash;
        On.Celeste.Player.Die -= OneDashBerry.OnDeath;
        On.Celeste.Level.TransitionTo -= OneDashBerry.OnTransition;
        On.Celeste.Player.Render -= OneDashBerry.OnRender;
    }

    public override void LoadContent(bool firstLoad) {
        base.LoadContent(firstLoad);
        OneDashBerry.indicator = new Image(GFX.Game["CoreOneDashBerry/fail"]);
    }
}