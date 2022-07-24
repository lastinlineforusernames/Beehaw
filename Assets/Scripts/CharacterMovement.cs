using UnityEngine;

namespace Beehaw.Character
{
    public class CharacterMovement : MonoBehaviour
    {

        private Rigidbody2D rigidbody;
        private OnGroundChecker groundChecker;

        [Header("Movement")]
        [SerializeField, Range(0, 20f)] private float maxSpeed = 12f;
        [SerializeField, Range(0, 100f)] private float maxAcceleration = 66f;
        [SerializeField, Range(0, 100f)] private float maxDecceleration = 71f;
        [SerializeField, Range(0, 100f)] private float maxTurnSpeed = 40f;
        [SerializeField, Range(0, 10f)] private float friction;

        [Header("Air Movement")]
        [SerializeField, Range(0, 100f)] private float maxAirAcceleration = 23f;
        [SerializeField, Range(0, 100f)] private float maxAirDecceleration = 24f;
        [SerializeField, Range(0, 100f)] private float maxAirTurnSpeed = 17f;

        private float horizontalInput;
        private Vector2 desiredVelocity;
        private Vector2 actualVelocity;
        private float maxSpeedChange;
        private float acceleration;
        private float decceleration;
        private float turnSpeed;
        private bool isOnGround;
        private bool isMoving;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            groundChecker = GetComponent<OnGroundChecker>();
        }

        private void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");

            FlipCharacter();

            desiredVelocity = new Vector2(horizontalInput, 0f) * Mathf.Max(maxSpeed - friction, 0f);
        }

        private void FixedUpdate()
        {
            isOnGround = groundChecker.IsOnGround();

            actualVelocity = rigidbody.velocity;

            HandleHorizontalMovement();
        }

        private void HandleHorizontalMovement()
        {
            acceleration = isOnGround ? maxAcceleration : maxAirAcceleration;
            decceleration = isOnGround ? maxDecceleration : maxAirDecceleration;
            turnSpeed = isOnGround ? maxTurnSpeed : maxAirTurnSpeed;

            if (isMoving)
            {
                if (Mathf.Sign(horizontalInput) != Mathf.Sign(actualVelocity.x))
                {
                    maxSpeedChange = turnSpeed * Time.deltaTime;
                }
                else
                {
                    maxSpeedChange = acceleration * Time.deltaTime;
                }
            }
            else
            {
                maxSpeedChange = decceleration * Time.deltaTime;
            }

            actualVelocity.x = Mathf.MoveTowards(actualVelocity.x, desiredVelocity.x, maxSpeedChange);

            rigidbody.velocity = actualVelocity;
            Debug.Log(desiredVelocity.x + " " + actualVelocity.x);
        }

        private void FlipCharacter()
        {
            if (Mathf.Abs(horizontalInput) > 0.01f)
            {
                transform.localScale = new Vector3(horizontalInput > 0 ? 1 : -1, 1, 1);
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
    }
}