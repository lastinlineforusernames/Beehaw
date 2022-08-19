using Beehaw.Character;
using UnityEngine;

namespace Beehaw.Level
{
    public class KillBox : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.Die();
                }
            }
        }

    }
}