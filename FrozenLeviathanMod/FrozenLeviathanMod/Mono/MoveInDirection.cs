using UnityEngine;

namespace FrozenLeviathanMod.Mono
{
    internal class MoveInDirection : MonoBehaviour
    {
        public Vector3 mps;

        private void Update()
        {
            transform.position += mps * Time.deltaTime;
        }
    }
}
