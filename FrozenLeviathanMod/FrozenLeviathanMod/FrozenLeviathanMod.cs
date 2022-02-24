using QModManager.API.ModLoading;

namespace FrozenLeviathanMod
{
    [QModCore]
    public static class FrozenLeviathanMod
    {
        [QModPatch]
        public static void Entry()
        {
            UnityEngine.Debug.Log("Frozen leviathan mod successfully loaded!");
        }
    }
}