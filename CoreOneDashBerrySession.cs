using YamlDotNet.Serialization;

namespace Celeste.Mod.CoreOneDashBerry;

public class CoreOneDashBerrySession : EverestModuleSession {
    public bool SpawnBerry = true;
    [YamlIgnore]
    public bool DashedTwice = false;
}