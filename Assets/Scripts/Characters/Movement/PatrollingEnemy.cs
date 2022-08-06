using UnityEngine;

namespace Beehaw.Character
{
    public class PatrollingEnemy : CharacterMovement
    {

        [SerializeField] private bool checksFront;
        [SerializeField] private bool checksLedge;

        public PatrollingEnemy()
        {
            horizontalInput = -1;
        }

        protected override void UpdateHorizontalMovement()
        {
            
            if (shouldFaceLeft())
            {
                horizontalInput = -1;
            }
            else if (shouldFaceRight())
            {
                horizontalInput = 1;
            }

        }

        protected virtual bool isMovingRight()
        {
            return horizontalInput > 0;
        }

        protected virtual bool shouldFaceRight()
        {
            return (
                (checksLedge && !groundChecker.IsLeftSideOnGround()) 
                || 
                (!isMovingRight() && checksFront && groundChecker.WillHitWall())
                );
        }

        protected virtual bool shouldFaceLeft()
        {
            return (
                (checksLedge && !groundChecker.IsRightSideOnGround()) 
                || 
                (isMovingRight() && checksFront && groundChecker.WillHitWall())
                );
        }
    }
}