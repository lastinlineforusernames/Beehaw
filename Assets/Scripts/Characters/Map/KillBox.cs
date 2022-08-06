using Beehaw.Character;
using UnityEngine;

namespace Beehaw.Level
{
    public class KillBox : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.Die();
                }
            }
        }

    }
}