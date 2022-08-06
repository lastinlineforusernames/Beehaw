using UnityEngine;

namespace Beehaw.Character
{
    public class CharacterJump : MonoBehaviour
    {
        private const float CoyoteTimeMaxTime = 0.03f;
        
        private Rigidbody2D rigidbody;
        private CollisionChecker groundChecker;
        private Vector2 actualVelocity;

        [Header("Jump")]
        [SerializeField, Range(0, 20f)] private float maxJumpHeight = 4.4f;
        [SerializeField, Range(0, 1.25f)] private float jumpDuration = 0.67833f;
        [SerializeField, Range(0, 5f)] private float jumpGravityMultiplier = 1f;
        [SerializeField, Range(1f, 10f)] private float fallGravityMultiplier = 4.3f;
        [SerializeField, Range(0, 1)] private int maxAirJumps = 0;
        [SerializeField, Range(1f, 10f)] private float jumpCutoff = 4f;
        [SerializeField, Range(1f, 20f)] private float terminalVelocity = 10f;
        [SerializeField, Range(0, 0.3f)] private float coyoteTime = 0.2f;
        [SerializeField, Range(0f, 0.3f)] private float jumpBuffer = 0.2f;

        private float jumpSpeed;
        private float gravityScale;
        private float gravityMultiplier;
        private bool canJumpAgain = false;
        private bool desiredJump;
        private float jumpBufferTimer;
        private float coyoteTimeTimer;
        private bool isPressingJump;
        private bool isOnGround;
        private bool isJumping;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            groundChecker = GetComponent<CollisionChecker>();
            gravityScale = 1f;
        }

        private void GetJumpInput()
        {
            if (Input.GetButtonDown("Jump"))
            {
                desiredJump = true;
                isPressingJump = true;
            }

            if (Input.GetButtonUp("Jump"))
            {
                isPressingJump = false;
            }
        }

        private void Update()
        {
            GetJumpInput();

            SetupPhysics();

            isOnGround = groundChecker.IsOnGround();

            HandleDesiredJump();

            HandleCoyoteTime();
        }

        private void HandleCoyoteTime()
        {
            if (!isJumping && !isOnGround)
            {
                coyoteTimeTimer += Time.deltaTime;
            }
            else
            {
                coyoteTimeTimer = 0;
            }
        }

        private void HandleDesiredJump()
        {
            if (jumpBuffer > 0)
            {
                if (desiredJump)
                {
                    jumpBufferTimer += Time.deltaTime;

                    if (jumpBufferTimer > jumpBuffer)
                    {
                        desiredJump = false;
                        jumpBufferTimer = 0;
                    }
                }
            }
        }

        private void SetupPhysics()
        {
            Vector2 newGravity = new Vector2(0, (-2 * maxJumpHeight) / (jumpDuration * jumpDuration));
            rigidbody.gravityScale = (newGravity.y / Physics2D.gravity.y) * gravityMultiplier;
        }

        private void FixedUpdate()
        {
            actualVelocity = rigidbody.velocity;

            if (desiredJump)
            {
                Jump();

                rigidbody.velocity = actualVelocity;
                return;
            }

            CalculateGravity();
        }

        private void CalculateGravity()
        {
            if (rigidbody.velocity.y > 0.01f)
            {
                if (isOnGround)
                {
                    gravityMultiplier = gravityScale;
                }
                else
                {
                    if (isPressingJump && isJumping)
                    {
                        gravityMultiplier = jumpGravityMultiplier;
                    }
                    else
                    {
                        gravityMultiplier = jumpCutoff;
                    }
                }
            }
            else if (rigidbody.velocity.y < -0.01f)
            {
                if (isOnGround)
                {
                    gravityMultiplier = gravityScale;
                }
                else
                {
                    gravityMultiplier = fallGravityMultiplier;
                }
            }
            else
            {
                if (isOnGround)
                {
                    isJumping = false;
                }
                gravityMultiplier = gravityScale;
            }
            rigidbody.velocity = new Vector3(actualVelocity.x, Mathf.Clamp(actualVelocity.y, -terminalVelocity, terminalVelocity));
        }

        private void Jump()
        {
            if (isOnGround || (coyoteTimeTimer > CoyoteTimeMaxTime && coyoteTimeTimer < coyoteTime) || canJumpAgain)
            {
                desiredJump = false;
                jumpBufferTimer = 0;
                coyoteTimeTimer = 0;

                canJumpAgain = (maxAirJumps == 1 && canJumpAgain == false);

                jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * rigidbody.gravityScale * maxJumpHeight);

                if (actualVelocity.y > 0)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - actualVelocity.y, 0);
                }
                else if (actualVelocity.y < 0)
                {
                    jumpSpeed += Mathf.Abs(rigidbody.velocity.y);
                }

                actualVelocity.y += jumpSpeed;
                isJumping = true;
                FMODUnity.RuntimeManager.PlayOneShot("Event:/Jump");
            }

            if (jumpBuffer == 0)
            {
                desiredJump = false;
            }
        }
    }
}