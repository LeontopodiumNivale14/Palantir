using System.Numerics;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;

namespace Palantir;

public sealed class Ipc(IDalamudPluginInterface plugin, DeepDungeon dungeon) : IDisposable
{
    private readonly ICallGateProvider<Vector3[]> _traps = plugin.GetIpcProvider<Vector3[]>("Palantir.Traps");

    public void Start() => _traps.RegisterFunc(() => dungeon.KnownTraps);

    public void Dispose() => _traps.UnregisterFunc();
}
