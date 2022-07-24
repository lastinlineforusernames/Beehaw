using UnityEngine;

namespace Beehaw.Character
{
    public class OnGroundChecker : MonoBehaviour
    {
        private bool isOnGround;

        [SerializeField] private float groundCheckRayLength = 0.9f;
        [SerializeField] private Vector3 collisionOffset;

        [SerializeField] private LayerMask groundLayer;

        private void Update()
        {
            bool isRightSideOnGround = Physics2D.Raycast(transform.position + collisionOffset, Vector2.down, groundCheckRayLength, groundLayer);
            bool isLeftSideOnGround = Physics2D.Raycast(transform.position - collisionOffset, Vector2.down, groundCheckRayLength, groundLayer);
            isOnGround = isLeftSideOnGround || isRightSideOnGround;
        }

        public bool IsOnGround()
        {
            return isOnGround;
        }

        private void OnDrawGizmos()
        {
            if (isOnGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
            Gizmos.DrawLine(transform.position + collisionOffset, transform.position + collisionOffset + Vector3.down * groundCheckRayLength);
            Gizmos.DrawLine(transform.position - collisionOffset, transform.position - collisionOffset + Vector3.down * groundCheckRayLength);
        }
    }
}