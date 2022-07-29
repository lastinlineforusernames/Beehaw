using Beehaw.Managers;
using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class PlayerHealth : Health
    {
        private GameManager gameManager;
        private GameHud gameHud; 


        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameHud = GameObject.Find("UIManager").GetComponent<GameHud>();
        }

        private void Start()
        {
            gameHud.updateHealth(getHealthPoints());
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
                applyDamage(1);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                applyDamage(1);
            }
        }
    }
}