using ECCLibrary;
using ECCLibrary.Mono;
using UnityEngine;
using FrozenLeviathanMod.Mono;

namespace FrozenLeviathanMod.Prefabs
{
    internal class FrozenLeviathanPrefab : AmphibiousCreatureAsset
    {
        public FrozenLeviathanPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override bool AllowSwimming => true;

        public override BehaviourType BehaviourType => BehaviourType.Leviathan;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Medium;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(40f, 5f, 40f), 4f, 3f, 0.1f);

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.9f, true, 17f);

        public override float Mass => 5000f;

        public override bool WalkUnderwater => false;

        public override float MaxVelocityForSpeedParameter => 30f;

        public override float MaxSurfaceAngle => 30f;

        public override MoveOnSurfaceSettings MoveOnSurfaceSettings => new MoveOnSurfaceSettings(0.2f, 5f, 3f, 30f, 30f, false);

        public override float TurnSpeedHorizontal => 0.5f;

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(8f, 4f, 2f);

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            var frozenLeviathan = prefab.AddComponent<FrozenLeviathan>();

            components.creatureDeath.sink = false;
            foreach (var r in prefab.GetComponentsInChildren<Renderer>())
            {
                for(int i = 0; i < r.materials.Length; i++)
                {
                    r.materials[i].EnableKeyword("MARMO_SPECMAP");
                    r.materials[i].SetColor(ShaderPropertyID._Color, new Color(2f, 2f, 2f));
                    r.materials[i].SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f));
                    r.materials[i].SetColor("_Shininess", new Color(0.5f, 0.5f, 0.5f));
                }
            }

            var roar = prefab.AddComponent<FrozenLeviathanRoar>();
            roar.evaluatePriority = 0.6f;
            AddWalkBehaviour(roar);
            frozenLeviathan.roar = roar;

            components.locomotion.rotateToSurfaceNormal = false;

            var trailRoot = prefab.SearchChild("Tail");
            var tailTrail = CreateTrail(prefab.SearchChild("Tail"), components, 5f);
            var trailFixer = trailRoot.AddComponent<FrozenLeviathanTrailFixer>();
            trailFixer.trailManager = tailTrail;
            trailFixer.surfaceMovement = prefab.GetComponent<OnSurfaceMovement>();

            var animateWalking = prefab.AddComponent<AnimateWalkingCreature>();
            animateWalking.animationMaxSpeed = MaxVelocityForSpeedParameter;

            var cinematicKill = prefab.AddComponent<FrozenLeviathanCinematicKill>();
            cinematicKill.creature = components.creature;
            cinematicKill.frozenLeviathan = frozenLeviathan;

            var onTouch = prefab.SearchChild("CinematicKillTrigger").AddComponent<OnTouch>();
            onTouch.onTouch = new OnTouch.OnTouchEvent();

            cinematicKill.onTouch = onTouch;
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 5000f;
        }
    }

    /* priorities chart:
     * swim random: 0.1
     * move on surface: 0.2
     * roar: 0.6
     * avoid obstacles: 0.9
     * 
    */
}
