using System;
using UnityEngine;

namespace Beehaw.Character
{
    [RequireComponent(typeof(OnGroundChecker))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Components")]
        protected Rigidbody2D rigidbody;
        protected OnGroundChecker groundChecker;

        [Header("Movement")]
        [SerializeField, Range(0, 20f)] protected float maxSpeed = 12f;
        [SerializeField, Range(0, 100f)] protected float maxAcceleration = 66f;
        [SerializeField, Range(0, 100f)] protected float maxDecceleration = 71f;
        [SerializeField, Range(0, 100f)] protected float maxTurnSpeed = 40f;
        [SerializeField, Range(0, 10f)] protected float friction;

        [Header("Air Movement")]
        [SerializeField, Range(0, 100f)] protected float maxAirAcceleration = 23f;
        [SerializeField, Range(0, 100f)] protected float maxAirDecceleration = 24f;
        [SerializeField, Range(0, 100f)] protected float maxAirTurnSpeed = 17f;

        [Header("Calculations and Checks")]
        protected float horizontalInput;
        protected float verticalInput;
        protected Vector2 desiredVelocity;
        protected Vector2 actualVelocity;
        protected float maxSpeedChange;
        protected float acceleration;
        protected float decceleration;
        protected float turnSpeed;
        protected bool isOnGround;
        protected bool isMoving;

        protected void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            groundChecker = GetComponent<OnGroundChecker>();
        }

        protected virtual void Update()
        {
            UpdateHorizontalMovement();
            UpdateVerticalMovement();

            FlipCharacter();

            desiredVelocity = new Vector2(horizontalInput, verticalInput) * Mathf.Max(maxSpeed - friction, 0f);
        }

        protected virtual void UpdateVerticalMovement()
        {
            verticalInput = 0f;
        }

        protected virtual void UpdateHorizontalMovement()
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        protected virtual void FixedUpdate()
        {
            isOnGround = groundChecker.IsOnGround();

            actualVelocity = rigidbody.velocity;

            HandleHorizontalMovement();
            
        }

        protected void HandleHorizontalMovement()
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
            
        }

        protected void FlipCharacter()
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