using Microsoft.Xna.Framework;
using Celeste.Mod.Entities;

namespace Celeste.Mod.CoreOneDashBerry;

[RegisterStrawberry(false, true)]
public class OneDashBerry : Strawberry {
    public static bool DashedTwice;

    public OneDashBerry() : base(new() { Name = "memorialTextController", Position = new(14197, -7363) }, new(3, 5), new("space", 1073741822)) {
        System.Action<Vector2> maddy = Get<DashListener>().OnDash;
        Get<DashListener>().OnDash = delegate (Vector2 dir) {
            if(SceneAs<Level>().Tracker.GetEntity<Player>().Dashes == 0) {
                maddy(dir);
            }
        };
    }

    internal static int OnDash(On.Celeste.Player.orig_StartDash orig, Player p) {
        int o = orig(p);
        if(p.Dashes == 0) {
            DashedTwice = true;
        }
        return o;
    }

    internal static PlayerDeadBody OnDeath(On.Celeste.Player.orig_Die orig, Player p, Vector2 dir, bool evenIfInvincible, bool registerDeathInStats) {
        DashedTwice = false;
        return orig(p, dir, evenIfInvincible, registerDeathInStats);
    }

    internal static void TransitionHook(On.Celeste.Level.orig_TransitionTo orig, Level level, LevelData next, Vector2 direction) {
        orig(level, next, direction);
        if(DashedTwice) {
            CoreOneDashBerry.Session.SpawnBerry = false;
        }
        if(level.Session.Area.SID == "Celeste/9-Core" && next.Name == "space" && level.Session.StartedFromBeginning && CoreOneDashBerry.Session.SpawnBerry) {
            level.Add(new OneDashBerry() { Tag = Tags.Persistent });
        }
    }
}