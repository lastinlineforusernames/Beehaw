using UnityEngine;

namespace Beehaw.Character
{
    public class OnGroundChecker : MonoBehaviour
    {
        [Header("On Ground Check")]
        private bool isOnGround;
        private bool isRightSideOnGround;
        private bool isLeftSideOnGround;
        [SerializeField] private float groundCheckRayLength = 0.9f;
        [SerializeField] private Vector3 groundCheckOffset;

        [Header("Forward Check")]
        private bool willHitWall;
        private bool willHitWallTop;
        private bool willHitWallBottom;
        [SerializeField] private float forwardCheckRayLength = 0.9f;
        [SerializeField] private Vector3 forwardCheckOffset;
        [SerializeField] private Vector3 verticalOffset;

        [Header("Layer Mask")]
        [SerializeField] private LayerMask groundLayer;

        private void Update()
        {
            CheckGroundCollision();
            CheckForwardCollision();
        }

        private void CheckForwardCollision()
        {
            willHitWallBottom = Physics2D.Raycast(transform.position - forwardCheckOffset + verticalOffset, Vector2.right * Mathf.Sign(transform.localScale.x), forwardCheckRayLength, groundLayer);
            willHitWallTop = Physics2D.Raycast(transform.position + forwardCheckOffset + verticalOffset, Vector2.right * Mathf.Sign(transform.localScale.x), forwardCheckRayLength, groundLayer);
            willHitWall = willHitWallBottom || willHitWallTop;
        }

        private void CheckGroundCollision()
        {
            isRightSideOnGround = Physics2D.Raycast(transform.position + groundCheckOffset, Vector2.down, groundCheckRayLength, groundLayer);
            isLeftSideOnGround = Physics2D.Raycast(transform.position - groundCheckOffset, Vector2.down, groundCheckRayLength, groundLayer);
            isOnGround = isLeftSideOnGround || isRightSideOnGround;
        }

        public bool IsOnGround()
        {
            return isOnGround;
        }

        public bool IsRightSideOnGround()
        {
            return isRightSideOnGround;
        }

        public bool IsLeftSideOnGround()
        {
            return isLeftSideOnGround; 
        }

        public bool WillHitWall()
        {
            return willHitWall;
        }

        private void OnDrawGizmos()
        {
            if (isOnGround) 
            {
                Gizmos.color = Color.green; 
            } 
            else 
            { 
                Gizmos.color = Color.red; 
            }
            Gizmos.DrawLine(transform.position + groundCheckOffset, transform.position + groundCheckOffset + Vector3.down * groundCheckRayLength);
            Gizmos.DrawLine(transform.position - groundCheckOffset, transform.position - groundCheckOffset + Vector3.down * groundCheckRayLength);
            Gizmos.DrawLine(transform.position + forwardCheckOffset + verticalOffset, transform.position + forwardCheckOffset + verticalOffset + Vector3.right * forwardCheckRayLength * Mathf.Sign(transform.localScale.x));
            Gizmos.DrawLine(transform.position - forwardCheckOffset + verticalOffset, transform.position - forwardCheckOffset + verticalOffset + Vector3.right * forwardCheckRayLength * Mathf.Sign(transform.localScale.x));
        }

    }
}