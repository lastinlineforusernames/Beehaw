using Beehaw.Level;
using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class WasperadoController : CharacterController
    {
        [Header("Components")]
        [SerializeField] private BossFightBounds fightBounds;
        private Collider2D collider;
        private GameObject player;

        [Header("AI - RNG")]
        [SerializeField, Range(0, 6)] private int shotsToFire;
        [SerializeField, Range(0, 0.5f)] private float delayBetweenShots;
        [SerializeField, Range(0, 2f)] private float holdJumpTime;
        [SerializeField, Range(0, 5f)] private float timeBeforeDoubleJump;
        [SerializeField, Range(0, 5f)] private float timeToRun;
        [SerializeField, Range(0, 5f)] private float delayBetweenActions;

        [Header("Calculations and Checks")]
        private float holdJumpTimer;
        private float runTimer;
        private bool shouldRun;
        private float transformOffset;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
            transformOffset = collider.bounds.extents.x;
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        protected override void Update()
        {
            if (fightBounds.IsBossFightStarted() && player != null)
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, 
                    fightBounds.getMinX() + transformOffset, 
                    fightBounds.getMaxX() - transformOffset), 
                    transform.position.y, 0);
                // TODO Setup action timers and logic for behaviors
                
                horizontalInput = evaluateMoveDirection();
                verticalInput = 0;
                isJumpButtonPressed = Random.value < 0.001f;
                isJumpButtonReleased = holdJumpTimer < 0;
                shouldFirePrimary = Random.value < 0.001f;
                shouldFireSecondary = false;
                if (isJumpButtonPressed)
                {
                    holdJumpTimer = holdJumpTime;
                }
                holdJumpTimer -= Time.deltaTime;
            }
        }

        private float evaluateMoveDirection()
        {
            float moveDirection;
            if(transform.position.x - 1 < fightBounds.getMinX())
            {
                moveDirection = 1;
            }
            else if (transform.position.x + 1 > fightBounds.getMaxX())
            {
                moveDirection = -1;
            }
            else
            {
                moveDirection = (player.transform.position - transform.position).normalized.x;
            }
            return moveDirection;
        }
    }
}