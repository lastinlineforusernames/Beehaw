using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class PatrollingEnemy : CharacterMovement
    {

        [SerializeField] private bool checksFront;
        [SerializeField] private bool checksLedge;

        public PatrollingEnemy()
        {
            horizontalInput = 1;
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

        private bool isMovingRight()
        {
            return horizontalInput > 0;
        }

        private bool shouldFaceRight()
        {
            return (
                (checksLedge && !groundChecker.IsLeftSideOnGround()) 
                || 
                (!isMovingRight() && checksFront && groundChecker.WillHitWall())
                );
        }

        private bool shouldFaceLeft()
        {
            return (
                (checksLedge && !groundChecker.IsRightSideOnGround()) 
                || 
                (isMovingRight() && checksFront && groundChecker.WillHitWall())
                );
        }
    }
}