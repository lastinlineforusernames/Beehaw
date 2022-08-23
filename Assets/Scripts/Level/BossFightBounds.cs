using Beehaw.Managers;
using UnityEngine;

namespace Beehaw.Level
{
    public class BossFightBounds : MonoBehaviour
    {
        private bool isBossFightStarted;
        private Collider2D collider;
        private AudioManager audioManager;
        private GameManager gameManager;
        [SerializeField] private GameObject boss;
        [SerializeField] private bool isFinalBoss;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public float getMinX()
        {
            return collider.bounds.center.x - collider.bounds.extents.x;
        }

        public float getMaxX()
        {
            return collider.bounds.center.x + collider.bounds.extents.x;
        }

        public bool IsBossFightStarted()
        {
            return isBossFightStarted;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isBossFightStarted = true;
                audioManager.playBossFightBGM();
            }
        }

        private void Update()
        {
            if (isBossFightStarted)
            {
                if (boss == null && isFinalBoss)
                {
                    gameManager.GameWin();
                }
            }
        }
    }
}