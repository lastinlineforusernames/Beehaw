using UnityEngine;

namespace Beehaw.Character
{
    public class CollisionChecker : MonoBehaviour
    {
        [Header("On Ground Check")]
        private bool isRightSideOnGround;
        private bool isLeftSideOnGround;
        [SerializeField] private float groundCheckRayLength = 0.9f;
        [SerializeField] private Vector3 groundCheckOffset;

        [Header("Forward Check")]
        private bool willHitWallTop;
        private bool willHitWallBottom;
        [SerializeField] private float forwardCheckRayLength = 0.9f;
        [SerializeField] private Vector3 forwardCheckOffset;
        [SerializeField] private Vector3 verticalOffset;

        [Header("Layer Mask")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask enemyLayer;
        private LayerMask collisionLayer; 
        
        private void Awake()
        {
            collisionLayer = groundLayer + enemyLayer;
        }

        private void Update()
        {
            CheckGroundCollision();
            CheckForwardCollision();
        }

        private void CheckForwardCollision()
        {
            willHitWallBottom = Physics2D.Raycast(transform.position - forwardCheckOffset + verticalOffset, Vector2.right * Mathf.Sign(transform.localScale.x), forwardCheckRayLength, collisionLayer);
            willHitWallTop = Physics2D.Raycast(transform.position + forwardCheckOffset + verticalOffset, Vector2.right * Mathf.Sign(transform.localScale.x), forwardCheckRayLength, collisionLayer);
        }

        private void CheckGroundCollision()
        {
            isRightSideOnGround = Physics2D.Raycast(transform.position + groundCheckOffset, Vector2.down, groundCheckRayLength, collisionLayer);
            isLeftSideOnGround = Physics2D.Raycast(transform.position - groundCheckOffset, Vector2.down, groundCheckRayLength, collisionLayer);
        }

        public bool IsOnGround()
        {
            return isLeftSideOnGround || isRightSideOnGround;
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
            return willHitWallBottom || willHitWallTop;
        }

        private void OnDrawGizmos()
        {
            if (IsOnGround()) 
            {
                Gizmos.color = Color.green; 
            } 
            else 
            { 
                Gizmos.color = Color.red; 
            }
            Gizmos.DrawLine(transform.position + groundCheckOffset, transform.position + groundCheckOffset + Vector3.down * groundCheckRayLength);
            Gizmos.DrawLine(transform.position - groundCheckOffset, transform.position - groundCheckOffset + Vector3.down * groundCheckRayLength);
            Gizmos.DrawLine(transform.position + forwardCheckOffset + verticalOffset, transform.position + forwardCheckOffset + verticalOffset + forwardCheckRayLength * Mathf.Sign(transform.localScale.x) * Vector3.right);
            Gizmos.DrawLine(transform.position - forwardCheckOffset + verticalOffset, transform.position - forwardCheckOffset + verticalOffset + forwardCheckRayLength * Mathf.Sign(transform.localScale.x) * Vector3.right);
        }

    }
}