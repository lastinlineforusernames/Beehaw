using Beehaw.Managers;
using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class PlayerMovement : CharacterMovement
    {
        [Header("Damage")]
        [SerializeField, Range(0, 500f)] private float knockbackForceX = 250f;
        [SerializeField, Range(0, 500f)] private float knockbackForceY = 150f;
        [SerializeField, Range(0, 1f)] private float oneSecondGhostTime;
        private float ghostTimer;
        private bool isOneSecondGhost;

        protected override void Update()
        {
            base.Update();

            ghostTimer -= Time.deltaTime;

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                Transform enemy = collision.transform;
                if (ghostTimer < 0)
                {
                    ghostTimer = oneSecondGhostTime;
                    AddKnockback(enemy);
                    Physics2D.IgnoreLayerCollision(GameManager.PlayerLayerMask, GameManager.EnemyLayerMask, true);
                    StartCoroutine(ResetCollision(oneSecondGhostTime));
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Transform enemy = collision.transform;
                if (ghostTimer < 0)
                {
                    ghostTimer = oneSecondGhostTime;
                    AddKnockback(enemy);
                }
            }    
        }

        private void AddKnockback(Transform instigator)
        {
            rigidbody.velocity = Vector2.zero;
            Vector2 knockbackDirection = transform.position - instigator.position;
            knockbackDirection = (knockbackDirection.normalized * Vector2.right * knockbackForceX) + (Vector2.up * knockbackForceY);
            rigidbody.AddForce(knockbackDirection, ForceMode2D.Impulse);
        }

        private IEnumerator ResetCollision(float delay)
        {
            yield return new WaitForSeconds(delay);
            Physics2D.IgnoreLayerCollision(GameManager.PlayerLayerMask, GameManager.EnemyLayerMask, false);
        }
    }
}