using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class ProjectileAttack : MonoBehaviour
    {
        [Header("Components")]
        // replace with controller interface
        protected IController controller;

        [Header("Combat")]
        [SerializeField] protected ProjectileMovement projectile;
        [SerializeField] protected Transform projectileSpawnPoint;
        [SerializeField, Range(0.1f, 2f)] protected float fireDelay;
        [SerializeField, Range(1, 6)] protected int maxAmmoCount = 6;

        [Header("Calculations and Checks")]
        protected float fireDelayTimer = 0;
        protected bool isFacingRight;
        protected bool canFire = true;
        protected int ammo;

        protected virtual void Awake()
        {
            controller = GetComponent<IController>();
        }

        protected virtual void Start()
        {
            ammo = maxAmmoCount;
        }

        protected virtual void Update()
        {
            isFacingRight = transform.localScale.x == 1;
            if (!canFire)
            {
                UpdateFireTimer();
            }
            else
            {
                if (controller != null && controller.ShouldFirePrimary() && ammo > 0)
                {
                    FireProjectile();
                }
            }

            if (controller != null && controller.ShouldFireSecondary())
            {
                Reload();
            }

        }

        protected virtual void Reload()
        {
            if (ammo < maxAmmoCount)
            {
                ammo = maxAmmoCount;
            }
        }

        protected virtual void FireProjectile()
        {
            Instantiate(projectile, projectileSpawnPoint.position, isFacingRight ? Quaternion.identity : Quaternion.Euler(0, -180, 0));
            canFire = false;
            fireDelayTimer = 0;
            ammo -= 1;
            PlayProjectileAudio();
        }

        protected virtual void PlayProjectileAudio()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/WaspGunshot");
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