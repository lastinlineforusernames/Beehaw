using UnityEngine;

namespace Beehaw.Character
{
    public class CharacterProjectileAttack : MonoBehaviour
    {

        [Header("Combat")]
        [SerializeField] private ProjectileMovement projectile;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField, Range(0.1f, 2f)] private float fireDelay;
        [SerializeField, Range(1, 6)] private int maxAmmoCount = 6;
        private float fireDelayTimer = 0;
        private bool isFacingRight;
        private bool canFire = true;
        private int ammo;

        private void Start()
        {
            ammo = maxAmmoCount;
        }

        private void Update()
        {
            isFacingRight = transform.localScale.x == 1;
            if (!canFire)
            {
                if (fireDelayTimer > fireDelay)
                {
                    canFire = true;
                }
                fireDelayTimer += Time.deltaTime;
            }
            else
            {
                if (Input.GetButtonDown("Fire1") && ammo > 0)
                {
                    Instantiate(projectile, projectileSpawnPoint.position, isFacingRight ? Quaternion.identity : Quaternion.Euler(0, -180, 0));
                    canFire = false;
                    fireDelayTimer = 0;
                    ammo -= 1;
                }
            }

            if (Input.GetButtonDown("Fire2")) 
            {
                if(ammo < maxAmmoCount)
                {
                    ammo = maxAmmoCount;
                }
            }
            
        }

        public int GetAmmo()
        {
            return ammo;
        }
    }
}