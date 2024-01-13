using System;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.CoreOneDashBerry;

public class CoreOneDashBerry : EverestModule {
    public static CoreOneDashBerry Instance;
    public override Type SessionType => typeof(CoreOneDashBerrySession);
    public static CoreOneDashBerrySession Session => (CoreOneDashBerrySession) Instance._Session;

    public CoreOneDashBerry() {
        Instance = this;
        Logger.SetLogLevel("CoreOneDashBerry", 0);
    }

    public override void Load() {
        On.Celeste.Player.StartDash += OneDashBerry.OnDash;
        On.Celeste.Player.Die += OneDashBerry.OnDeath;
        On.Celeste.Level.TransitionTo += OneDashBerry.TransitionHook;
    }

    public override void Unload() {
        On.Celeste.Player.StartDash -= OneDashBerry.OnDash;
        On.Celeste.Player.Die -= OneDashBerry.OnDeath;
        On.Celeste.Level.TransitionTo -= OneDashBerry.TransitionHook;
    }
}