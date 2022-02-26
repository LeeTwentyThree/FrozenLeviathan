using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace FrozenLeviathanMod.Mono
{
    internal class FrozenLeviathanCinematicKill : MonoBehaviour
    {
        public FrozenLeviathan frozenLeviathan;
        public Creature creature;
        public OnTouch onTouch;

        private string _cinematicAttackParameter = "cin_attack";
        private string _playerAnimationParameter = "snowstalker_attack";
        private PlayerCinematicController _playerCinematic;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _playerCinematic = gameObject.AddComponent<PlayerCinematicController>();
            _playerCinematic.animatedTransform = gameObject.SearchChild("AttachBone").transform;
            _playerCinematic.animator = creature.GetAnimator();
            _playerCinematic.playerViewAnimationName = _playerAnimationParameter;
            _playerCinematic.animParam = _cinematicAttackParameter;
            _playerCinematic.animParamReceivers = new GameObject[0];
            onTouch.onTouch.AddListener(OnTouch);
        }

        public void OnTouch(Collider collider)
        {
            if (frozenLeviathan.roar.IsRoaring)
            {
                return;
            }
            var player = collider.GetComponentInParent<Player>();
            if (player && player.CanBeAttacked())
            {
                StartCoroutine(StartPlayerCinematic(player));
                return;
            }
        }

        public IEnumerator StartPlayerCinematic(Player player)
        {
            _playerCinematic.StartCinematicMode(player);
            _rb.isKinematic = true;
            yield return new WaitForSeconds(3.3f);
            _playerCinematic.EndCinematicMode();
            uGUI_PlayerDeath.main.TriggerDeathVignette(uGUI_PlayerDeath.DeathTypes.CutToBlack);
            player.liveMixin.TakeDamage(300f, transform.position, DamageType.Normal, creature.gameObject);
            _rb.isKinematic = false;
        }
    }
}
