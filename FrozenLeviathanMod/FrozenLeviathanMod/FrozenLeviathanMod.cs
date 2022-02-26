using QModManager.API.ModLoading;
using FrozenLeviathanMod.Mono;
using FrozenLeviathanMod.Prefabs;
using UnityEngine;
using ECCLibrary;
using System.Reflection;
using SMLHelper.V2.Commands;
using SMLHelper.V2.Handlers;

namespace FrozenLeviathanMod
{
    [QModCore]
    public static class FrozenLeviathanMod
    {
        internal static FrozenLeviathanPrefab frozenLeviathan;
        internal static FrozenLeviathanPrefab frozenLeviathanJuvenile;
        internal static AssetBundle assets;
        internal static Assembly myAssembly = Assembly.GetExecutingAssembly();
        
        [QModPatch]
        public static void Entry()
        {
            assets = ECCHelpers.LoadAssetBundleFromAssetsFolder(myAssembly, "frozenleviathan");

            frozenLeviathan = new FrozenLeviathanPrefab("FrozenLeviathan", "Frozen Leviathan", "We thought it was dead...", assets.LoadAsset<GameObject>("FrozenLeviathan_Prefab"), null);
            frozenLeviathan.Patch();

            frozenLeviathanJuvenile = new FrozenLeviathanPrefab("FrozenLeviathanJuvenile", "Frozen Leviathan Juvenile", "We thought it was dead... It reproduced...", assets.LoadAsset<GameObject>("FrozenLeviathanJuvenile_Prefab"), null);
            frozenLeviathanJuvenile.Patch();

            ConsoleCommandsHandler.Main.RegisterConsoleCommands(typeof(ModCommands));
        }
    }

    public static class ModCommands
    {
        [ConsoleCommand("dropships")]
        public static void SpawnDropShips()
        {
            DropShipCinematic.PlayCinematic();
        }

        [ConsoleCommand("froloattack")]
        public static void FroloAttack()
        {
            FroloDropshipCinematic.PlayCinematic();
        }
    }
}