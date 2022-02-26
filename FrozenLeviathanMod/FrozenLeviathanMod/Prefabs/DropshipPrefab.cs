using ECCLibrary;
using UnityEngine;

namespace FrozenLeviathanMod.Prefabs
{
    internal static class DropshipPrefab
    {
        public static GameObject GetDropship()
        {
            var dropShip = Object.Instantiate(FrozenLeviathanMod.assets.LoadAsset<GameObject>("Dropship_Prefab"));
            ECCHelpers.ApplySNShaders(dropShip, new UBERMaterialProperties(8f, 1f, 1f));
            dropShip.AddComponent<SkyApplier>().renderers = dropShip.GetComponentsInChildren<Renderer>();
            dropShip.transform.localScale *= 1.2f;
            return dropShip;
        }
    }
}
