using UnityEngine;

namespace Beehaw.Character
{
    public class FlyingEnemy : PatrollingEnemy
    {
        private GameObject player;

        protected override void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            base.Awake();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            HandleVerticalMovement();
        }

        private void HandleVerticalMovement()
        {
            actualVelocity.y = desiredVelocity.y;
            rigidbody.velocity = actualVelocity;
        }

        protected override void UpdateVerticalMovement()
        {
            if (player != null)
            {
                if (shouldMoveDown())
                {
                    verticalInput = -1;
                }
                else if (shouldMoveUp())
                {
                    verticalInput = 1;
                }
            }
            
        }
        protected override void UpdateHorizontalMovement()
        {
            if (player != null)
            {
                base.UpdateHorizontalMovement();
            }
        }

        protected override bool shouldFaceRight()
        {
            return player.transform.position.x > transform.position.x;
        }

        protected override bool shouldFaceLeft()
        {
            return player.transform.position.x < transform.position.x;
        }

        protected bool shouldMoveUp()
        {
            return player.transform.position.y > transform.position.y;
        }

        protected bool shouldMoveDown()
        {
            return player.transform.position.y < transform.position.y;
        }
    }
}