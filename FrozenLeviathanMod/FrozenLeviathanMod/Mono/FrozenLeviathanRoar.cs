using UnityEngine;

namespace FrozenLeviathanMod.Mono
{
    internal class FrozenLeviathanRoar : CreatureAction
    {
        public FrozenLeviathan frozenLeviathan;

        public float minDelay = 20f;
        public float maxDelay = 25f;
        public float _roarDuration = 10f;
        public bool canRandomlyRoar = true;

        private float _timeRoarAgain;
        private float _timeStopRoaring;
        private bool _isRoaring;

        public bool IsRoaring { get { return _isRoaring && Time.time < _timeStopRoaring; } }

        private Rigidbody _rb;

        private static int _roarParameter = Animator.StringToHash("roar");
        private static int _randomParameter = Animator.StringToHash("random");

        public override void Start()
        {
            base.Start();
            DelayRoar();
            _rb = GetComponent<Rigidbody>();
        }

        public void DelayRoar()
        {
            _timeRoarAgain = Time.time + Random.Range(minDelay, maxDelay);
        }

        public override float Evaluate(float time)
        {
            if (_isRoaring && Time.time < _timeStopRoaring)
            {
                return 1f;
            }
            if (Time.time < _timeRoarAgain)
            {
                return 0f;
            }
            if (!canRandomlyRoar)
            {
                return 0f;
            }
            return base.Evaluate(time);
        }

        public override void StartPerform(float time)
        {
            _timeStopRoaring = Time.time + _roarDuration;
            _isRoaring = true;
            _rb.isKinematic = true;
            PlayRoar();
        }

        public override void StopPerform(float time)
        {
            _isRoaring = false;
            DelayRoar();
            _rb.isKinematic = false;
        }

        public void PlayRoar()
        {
            var animator = creature.GetAnimator();
            animator.SetFloat(_randomParameter, Random.value);
            animator.SetTrigger(_roarParameter);
        }
    }
}
