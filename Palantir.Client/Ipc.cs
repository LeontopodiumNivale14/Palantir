using System.Numerics;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;

namespace Palantir;

public sealed class Ipc(IDalamudPluginInterface plugin, Storage storage) : IDisposable
{
    private readonly ICallGateProvider<ushort, Vector3[]> _traps = plugin.GetIpcProvider<ushort, Vector3[]>("Palantir.Traps");

    public void Start() => _traps.RegisterFunc(storage.Traps);

    public void Dispose() => _traps.UnregisterFunc();
}
