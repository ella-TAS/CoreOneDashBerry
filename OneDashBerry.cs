using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.Entities;

namespace Celeste.Mod.CoreOneDashBerry;

[RegisterStrawberry(false, true)]
public class OneDashBerry : Strawberry {
    internal static Image indicator;

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
            CoreOneDashBerry.Session.DashedTwice = true;
            if(p.SceneAs<Level>().Session.Area.SID == "Celeste/9-Core" && CoreOneDashBerry.Settings.KillOnFail && p.SceneAs<Level>().Session.LevelData.Name != "space") {
                p.Die(Vector2.Zero, true);
            }
        }
        return o;
    }

    internal static PlayerDeadBody OnDeath(On.Celeste.Player.orig_Die orig, Player p, Vector2 dir, bool evenIfInvincible, bool registerDeathInStats) {
        CoreOneDashBerry.Session.DashedTwice = false;
        return orig(p, dir, evenIfInvincible, registerDeathInStats);
    }

    internal static void OnTransition(On.Celeste.Level.orig_TransitionTo orig, Level level, LevelData next, Vector2 direction) {
        orig(level, next, direction);
        if(CoreOneDashBerry.Session.DashedTwice) {
            CoreOneDashBerry.Session.SpawnBerry = false;
        }
        if(level.Session.Area.SID == "Celeste/9-Core" && next.Name == "space" && level.Session.StartedFromBeginning && CoreOneDashBerry.Session.SpawnBerry) {
            level.Add(new OneDashBerry() { Tag = Tags.Persistent });
        }
    }

    internal static void OnRender(On.Celeste.Player.orig_Render orig, Player p) {
        orig(p);
        if(p.SceneAs<Level>().Session.Area.SID == "Celeste/9-Core" && CoreOneDashBerry.Settings.ShowFailIndicator && (CoreOneDashBerry.Session.DashedTwice || !CoreOneDashBerry.Session.SpawnBerry)) {
            indicator.Position = p.Position + new Vector2(-4, -24);
            indicator.Render();
        }
    }
}