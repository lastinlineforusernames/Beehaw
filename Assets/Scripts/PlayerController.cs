using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Member Variables
    private Rigidbody2D rigidbody;
    private bool isFacingRight = true;
    private const string HorizontalAxisName = "Horizontal";
    private float horizontalInput;
    private bool isJumping;
    private bool jumpInputReleased;
    private float lastOnGroundTimer;
    private float lastJumpTimer;
    private float gravityScale;
    #endregion

    #region Serialized Variables
    [Header("Movement")]
    [SerializeField]
    [Range(0, 100f)]
    private float movementSpeed;
    [SerializeField]
    [Range(0, 100f)]
    private float acceleration;
    [SerializeField]
    [Range(0, 100f)]
    private float decceleration;
    [SerializeField]
    [Range(0, 10f)]
    private float velocityPower;
    [SerializeField]
    [Range(0, 5f)]
    private float frictionAmount;

    [Header("Jump")]
    [SerializeField]
    [Range(0, 20f)]
    private float jumpForce;
    [SerializeField]
    [Range(0, 1f)]
    private float jumpCutMultiplier;
    [SerializeField]
    [Range(0, 1f)]
    private float coyoteTimeBuffer;
    [SerializeField]
    [Range(0, 1f)]
    private float jumpTimeBuffer;
    [SerializeField]
    [Range(0, 3f)]
    private float fallingGravityMultiplier;

    [Header("Combat")]
    [SerializeField]
    private ProjectileMovement projectile;
    [SerializeField]
    private Transform projectileSpawnPoint;

    [Header("Ground Check")]
    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private Vector2 groundCheckSize;
    [SerializeField]
    private LayerMask groundLayer;
    #endregion

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gravityScale = rigidbody.gravityScale;
    }


    private void Update()
    {

        gatherInput();

        collisionChecks();
        UpdateGravityScale();

        // handle jump
        if (lastOnGroundTimer > 0 && lastJumpTimer > 0 && !isJumping)
        {
            isJumping = true;
            Jump();
            
        }
        
        // update timers
        lastOnGroundTimer -= Time.deltaTime;
        lastJumpTimer -= Time.deltaTime;

        // projectile
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, projectileSpawnPoint.position, isFacingRight ? Quaternion.identity : Quaternion.Euler(0, -180, 0));
        }
    }

    private void FixedUpdate()
    {
        UpdateHorizontalMovement();
        UpdateFriction();
        UpdateSpriteDirection();
        
    }

    private void gatherInput()
    {
        horizontalInput = Input.GetAxis(HorizontalAxisName);
        if (Input.GetButtonDown("Jump"))
        {
            OnJumpPressed();
        }

        if (Input.GetButtonUp("Jump"))
        {
            OnJumpReleased();
        }
    }

    private void collisionChecks()
    {
        if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer))
        {
            lastOnGroundTimer = coyoteTimeBuffer;
            isJumping = false;
        }

        if (rigidbody.velocity.y < 0)
        {
            isJumping = false;
        }
    }

    private void UpdateGravityScale()
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.gravityScale = gravityScale * fallingGravityMultiplier;
        }
        else
        {
            rigidbody.gravityScale = gravityScale;
        }
    }

    private void Jump()
    {
        lastJumpTimer = 0.3f;
        lastOnGroundTimer = 0;
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpInputReleased = false;        
    }

    private void OnJumpPressed()
    {
        lastJumpTimer = jumpTimeBuffer;
    }

    private void OnJumpReleased()
    {
        Debug.Log(rigidbody.velocity.y + " " + isJumping);
        if (rigidbody.velocity.y > 0 && isJumping)
        {
            rigidbody.AddForce(Vector2.down * rigidbody.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            Debug.Log(Vector2.down * rigidbody.velocity.y * (1 - jumpCutMultiplier));
        }

        jumpInputReleased = true;
        lastJumpTimer = 0;
    }

    private void UpdateHorizontalMovement()
    {
        float targetSpeed = horizontalInput * movementSpeed;
        float speedDifference = targetSpeed - rigidbody.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.1f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, velocityPower) * Mathf.Sign(speedDifference);
        rigidbody.AddForce(movement * Vector2.right);
    }

    private void UpdateFriction()
    {
        if (lastOnGroundTimer > 0 && Mathf.Abs(horizontalInput) < 0.01f)
        {
            float friction = Mathf.Min(Mathf.Abs(rigidbody.velocity.x), Mathf.Abs(frictionAmount));
            friction *= Mathf.Sign(rigidbody.velocity.x);
            rigidbody.AddForce(Vector2.right * -friction, ForceMode2D.Impulse);
        }
    }

    void UpdateSpriteDirection()
    {
        if (!isFacingRight && horizontalInput > 0)
        {
            Flip();
        }
        else if (isFacingRight && horizontalInput < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
