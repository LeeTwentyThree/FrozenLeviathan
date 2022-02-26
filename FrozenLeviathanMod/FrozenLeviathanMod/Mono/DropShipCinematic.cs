using UnityEngine;
using FrozenLeviathanMod.Prefabs;

namespace FrozenLeviathanMod.Mono
{
    internal class DropShipCinematic : MonoBehaviour
    {
        private Vector3 _mainDropShipPosition = new Vector3(-42, 13f, 351);
        private Vector3[] _flyingDropShipPositions = new Vector3[]
        {
            new Vector3(60, 50, 300),
            new Vector3(40, 50, 330),
            new Vector3(20, 50, 290),
            new Vector3(4, 43, 327),
            new Vector3(16, 45, 373),
        };

        private Vector3 _flyingDropShipDirection = new Vector3(-0.8f, 0f, 0.3f);
        private float _flyingDropShipSpeed = 10f;

        private Vector3 _mainDropshipVelocity;
        private float _mainDropShipUpAcceleration = 2f;
        private float _mainDropShipForwardAcceleration = 18f;
        private Vector3 _mainDropShipForward = new Vector3(-0.8f, 0f, -0.6f);

        private MoveInDirection _mainDropShip;

        private float _ascendDelay = 2f;
        private float _forwardDelay = 8f;

        private float _timeStarted;

        public static void PlayCinematic()
        {
            var c = new GameObject("DropShipCinematic").AddComponent<DropShipCinematic>();
            Destroy(c.gameObject, 20f);
        }

        private void Start()
        {
            var dropShip1 = DropshipPrefab.GetDropship();
            dropShip1.transform.position = _mainDropShipPosition;
            dropShip1.transform.forward = _mainDropShipForward;
            _mainDropShip = dropShip1.AddComponent<MoveInDirection>();

            for (var i = 0; i < _flyingDropShipPositions.Length; i++)
            {
                var dropShip = DropshipPrefab.GetDropship();
                dropShip.transform.localPosition = _flyingDropShipPositions[i];
                dropShip.transform.forward = _flyingDropShipDirection;
                dropShip.AddComponent<MoveInDirection>().mps = _flyingDropShipDirection * _flyingDropShipSpeed;
            }

            _timeStarted = Time.time;
        }

        private void Update()
        {
            if (Time.time > _timeStarted + _forwardDelay)
            {
                _mainDropShip.mps += _mainDropShipForward * _mainDropShipForwardAcceleration * Time.deltaTime;
            }
            else if (Time.time > _timeStarted + _ascendDelay)
            {
                _mainDropShip.mps += Vector3.up * _mainDropShipUpAcceleration * Time.deltaTime;
            }
        }
    }
}
