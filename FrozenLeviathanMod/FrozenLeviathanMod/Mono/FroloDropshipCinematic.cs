using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UWE;

namespace FrozenLeviathanMod.Mono
{
    internal class FroloDropshipCinematic : MonoBehaviour
    {
        private Vector3 _spawnPos = new Vector3(-60f, 6f, 290f);
        private Vector3 _spawnEulers = new Vector3(0f, 0f, 0f);
        private FrozenLeviathan _frozenLeviathan;
        //private Vector3 _wanderTarget = new Vector3(-42, 13f, 351);
        private float _attackRadius = 20f;
        private bool _attacked = false;

        public static void PlayCinematic()
        {
            var c = new GameObject("FroloDropshipCinematic").AddComponent<FroloDropshipCinematic>();
        }

        private IEnumerator Start()
        {
            var task = PrefabDatabase.GetPrefabAsync(FrozenLeviathanMod.frozenLeviathanJuvenile.ClassID);
            yield return task;
            if (!task.TryGetPrefab(out var prefab))
            {
                ErrorMessage.AddMessage("Failed to load Frozen Leviathan Juvenile");
                yield break;
            }
            var spawned = Instantiate(prefab, _spawnPos, Quaternion.Euler(_spawnEulers));
            spawned.SetActive(true);
            _frozenLeviathan = spawned.GetComponent<FrozenLeviathan>();
            _frozenLeviathan.roar.canRandomlyRoar = false;
            yield return new WaitForEndOfFrame();
            _frozenLeviathan.GetComponent<Rigidbody>().isKinematic = true;
            yield return new WaitForSeconds(8f);
            _frozenLeviathan.roar.PlayRoar();
            yield return new WaitForSeconds(12f);
            _frozenLeviathan.GetComponent<Rigidbody>().isKinematic = false;
            var walk = _frozenLeviathan.GetComponent<WalkBehaviour>();
            walk.WalkTo(Player.main.transform.position, 30f);
            walk.StartTargetOverride(40f);
        }

        private void Update()
        {
            if (_attacked)
            {
                return;
            }
            if (_frozenLeviathan != null)
            {
                if (Vector3.Distance(Player.main.transform.position, _frozenLeviathan.transform.position) < _attackRadius)
                {
                    _attacked = true;
                    _frozenLeviathan.GetComponent<FrozenLeviathanCinematicKill>().StartPlayerCinematic(Player.main);
                }
            }
        }
    }
}
