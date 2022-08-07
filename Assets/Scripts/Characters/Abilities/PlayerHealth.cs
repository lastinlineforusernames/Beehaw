using Beehaw.Managers;
using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class PlayerHealth : Health
    {
        private GameManager gameManager;
        private GameHud gameHud;
        private Collider2D collider;
        private float oneSecondGhostTimer;
        private float oneSecondGhostTime = 1f;

        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameHud = GameObject.Find("UIManager").GetComponent<GameHud>();
            collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            gameHud.updateHealth(getHealthPoints());
        }

        private void Update()
        {
            oneSecondGhostTimer -= Time.deltaTime;    
        }

        public override void Die()
        {
            gameManager.GameOver();
            base.Die();
        }

        public override void applyDamage(int damageToApply)
        {
            base.applyDamage(damageToApply);
            gameHud.updateHealth(getHealthPoints());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (oneSecondGhostTimer < 0)
                {
                    oneSecondGhostTimer = oneSecondGhostTime;
                    applyDamage(1);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (oneSecondGhostTimer < 0)
                {
                    oneSecondGhostTimer = oneSecondGhostTime;
                    applyDamage(1);
                }
            }
        }

    }
}