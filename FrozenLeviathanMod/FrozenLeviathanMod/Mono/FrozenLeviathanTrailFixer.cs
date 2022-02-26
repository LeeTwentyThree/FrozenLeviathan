using UnityEngine;

namespace FrozenLeviathanMod.Mono
{
    internal class FrozenLeviathanTrailFixer : MonoBehaviour
    {
        public OnSurfaceMovement surfaceMovement;
        public TrailManager trailManager;
        public float waterSnapping = 1f;
        public float landSnapping = 3f;

        private void Update()
        {
            trailManager.segmentSnapSpeed = surfaceMovement.IsOnSurface() ? landSnapping : waterSnapping;
        }
    }
}
