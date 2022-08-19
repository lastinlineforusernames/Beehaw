using Beehaw.Managers;
using UnityEngine;

namespace Beehaw.Character
{
    [RequireComponent(typeof(PlayerController))]

    public class CharacterProjectileAttack : ProjectileAttack
    {
        [Header("Components")]
        private GameHud gameHud;

        [Header("Calculations & Checks")]
        private bool isReloading;
        private float reloadTimer;
        [SerializeField] private float reloadTime = 0.2f;
        private bool canPlayReloadAudio = true;

        protected override void Awake()
        {
            gameHud = GameObject.Find("UIManager").GetComponent<GameHud>();
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            gameHud.updateAmmo(ammo);
        }

        protected override void Update()
        {
            base.Update();
            if (isReloading && reloadTimer < 0)
            {
                reloadTimer = reloadTime;
                if (canPlayReloadAudio)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Reload");
                    canPlayReloadAudio = false;
                }
                if (ammo < maxAmmoCount)
                {
                    ammo += 1;
                }
                else
                {
                    isReloading = false;
                    canPlayReloadAudio = true;
                }
                gameHud.updateAmmo(GetAmmo());
            }
            reloadTimer -= Time.deltaTime;
        }

        protected override void Reload()
        {
            if (ammo < maxAmmoCount)
            {
                isReloading = true;
            }
        }

        protected override void FireProjectile()
        {
            base.FireProjectile();
            gameHud.updateAmmo(GetAmmo());

        }

        protected override void PlayProjectileAudio()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gunshot");
        }
    }
}