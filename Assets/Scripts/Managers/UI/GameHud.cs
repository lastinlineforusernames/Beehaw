using Beehaw.Character;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Beehaw.Managers
{
    public class GameHud : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        private CharacterProjectileAttack playerAttack;
        private int health;
        private int ammo;
        [SerializeField] private Sprite[] healthImageArray;
        [SerializeField] private Sprite[] ammoImageArray;
        [SerializeField] private Image healthImage;
        [SerializeField] private Image ammoImage;
        private int ammoArrayIndex = 6;
        private int healthArrayIndex = 10;

        private void Awake()
        {
            playerAttack = GameObject.Find("Player").GetComponent<CharacterProjectileAttack>();
            playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        }

        private void Start()
        {
            health = playerHealth.getHealthPoints();
            ammo = playerAttack.GetAmmo();
        }

        public void updateHealth(int health)
        {
            healthArrayIndex = health - 1;
            if (healthArrayIndex < 0)
            {
                healthArrayIndex = 0;
            }
            healthImage.sprite = healthImageArray[healthArrayIndex];
        }

        public void updateAmmo(int ammo)
        {
            ammoArrayIndex = ammo;
            if (ammoArrayIndex < 0)
            {
                ammoArrayIndex = 0;
            }
            ammoImage.sprite = ammoImageArray[ammoArrayIndex];
        }
    }
}