using UnityEngine;

namespace Beehaw.Character
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int healthPoints;
        private Camera camera;

        private void Start()
        {
            camera = Camera.main;
        }

        public int getHealthPoints()
        {
            return healthPoints;
        }

        public virtual void applyDamage(int damageToApply)
        {
            healthPoints -= damageToApply;
            if (healthPoints < 1)
            {
                Die();
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Damage");
            }
        }

        public virtual void Die()
        {
            if (camera != null)
            {
                Vector3 position = transform.position;
                Vector3 screenPosition = camera.WorldToScreenPoint(position);
                bool onScreen = screenPosition.x > 0f && screenPosition.x < Screen.width && screenPosition.y > 0f && screenPosition.y < Screen.height;

                if (onScreen)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Death");
                }
            }
            Destroy(gameObject);
        }

    }
}