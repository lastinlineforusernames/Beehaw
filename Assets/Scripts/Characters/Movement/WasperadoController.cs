using Beehaw.Level;
using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class WasperadoController : PlayerController
    {
        [Header("Components")]
        [SerializeField] private BossFightBounds fightBounds;
        private Collider2D collider;
        private CollisionChecker collisionChecker;
        private GameObject player;

        [Header("Combat Logic Inputs")]
        [SerializeField, Range(0, 6)] private int shotsToFire;
        [SerializeField, Range(0, 0.5f)] private float delayBetweenShots;
        [SerializeField, Range(0, 1f)] private float holdJumpTime;
        [SerializeField, Range(0, 2f)] private float timeBeforeDoubleJump;
        [SerializeField, Range(0, 2f)] private float timeToRun;
        [SerializeField, Range(0, 2f)] private float timeToJump;
        [SerializeField, Range(0, 2f)] private float timeToShoot;


        [Header("Calculations and Checks")]
        private int actionPhase = 0;
        private int currentActionPhase;
        private float actionTimer = 0;
        private float holdJumpTimer;
        private float transformOffset;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
            collisionChecker = GetComponent<CollisionChecker>();
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
                // Clamp position to fight volume
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, 
                    fightBounds.getMinX() + transformOffset, 
                    fightBounds.getMaxX() - transformOffset), 
                    transform.position.y, 0);
                // Combat Logic
                shouldFireSecondary = false;
                shouldFirePrimary = false;
                isJumpButtonPressed = false;
                currentActionPhase = actionPhase;
                switch (actionPhase)
                {
                    case 0: // Jump
                        if (actionTimer == 0)
                        {
                            horizontalInput = facePlayer();
                            isJumpButtonPressed = true;
                            holdJumpTimer = holdJumpTime;
                        }
                        if (collisionChecker.GetOnGroundTime() < 0.01f)
                        {
                            horizontalInput = 0;
                        }
                        if (actionTimer > timeToJump)
                        {
                            actionPhase = 1;
                        }
                        break;
                    case 1: // Run
                        if (actionTimer == 0)
                        {
                            horizontalInput = facePlayer();
                        }
                        if (actionTimer > timeToRun)
                        {
                            actionPhase = 2;
                        }
                        break;
                    case 2: // Shoot
                        if (actionTimer == 0)
                        {
                            horizontalInput = facePlayer();
                            shouldFireSecondary = true;
                            shouldFirePrimary = true;
                        } 
                        else if (actionTimer < timeToShoot)
                        {
                            shouldFirePrimary = true;
                        }
                        horizontalInput = 0;
                        if (actionTimer > timeToShoot)
                        {
                            actionPhase = 0;
                        }
                        break;
                }
                actionTimer += Time.deltaTime;
                if (actionPhase != currentActionPhase)
                {
                    actionTimer = 0;
                }
                isJumpButtonReleased = holdJumpTimer < 0;
                isJumpButtonPressed = !isJumpButtonReleased;
                holdJumpTimer -= Time.deltaTime;
            }
        }

        private float facePlayer()
        {
            return (player.transform.position - transform.position).normalized.x;
        }
    }
}