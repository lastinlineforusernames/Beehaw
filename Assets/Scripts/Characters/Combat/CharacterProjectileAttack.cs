using Beehaw.Managers;
using UnityEngine;

namespace Beehaw.Character
{
    [RequireComponent(typeof(CharacterController))]

    public class CharacterProjectileAttack : MonoBehaviour
    {
        [Header("Components")]
        private CharacterController controller;
        private GameHud gameHud;

        [Header("Combat")]
        [SerializeField] private ProjectileMovement projectile;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField, Range(0.1f, 2f)] private float fireDelay;
        [SerializeField, Range(1, 6)] private int maxAmmoCount = 6;

        [Header("Calculations and Checks")]
        private float fireDelayTimer = 0;
        private bool isFacingRight;
        private bool canFire = true;
        private int ammo;

        private void Awake()
        {
            gameHud = GameObject.Find("UIManager").GetComponent<GameHud>();
            controller = GetComponent<CharacterController>();
        }

        private void Start()
        {
            ammo = maxAmmoCount;
            gameHud.updateAmmo(ammo);
        }

        private void Update()
        {
            isFacingRight = transform.localScale.x == 1;
            if (!canFire)
            {
                UpdateFireTimer();
            }
            else
            {
                if (controller.ShouldFirePrimary() && ammo > 0)
                {
                    FireProjectile();
                }
            }

            if (controller.ShouldFireSecondary())
            {
                Reload();
            }

        }

        private void Reload()
        {
            if (ammo < maxAmmoCount)
            {
                // TODO reload one shot at a time and add interrupts
                ammo = maxAmmoCount;
                gameHud.updateAmmo(GetAmmo());
            }
        }

        private void FireProjectile()
        {
            Instantiate(projectile, projectileSpawnPoint.position, isFacingRight ? Quaternion.identity : Quaternion.Euler(0, -180, 0));
            canFire = false;
            fireDelayTimer = 0;
            ammo -= 1;
            gameHud.updateAmmo(GetAmmo());
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gunshot");
        }

        private void UpdateFireTimer()
        {
            if (fireDelayTimer > fireDelay)
            {
                canFire = true;
            }
            fireDelayTimer += Time.deltaTime;
        }

        public int GetAmmo()
        {
            return ammo;
        }
    }
}