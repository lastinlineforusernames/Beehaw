using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private bool isFacingRight = false;
    private float horizontalInput = 1;
    private float lastOnGroundTimer;

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

    [Header("Ground Check")]
    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private Vector2 groundCheckSize;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Transform ledgeCheckPoint;
    [SerializeField]
    [Range(0, 4f)]
    private float ledgeCheckDistance = 2f;
    [SerializeField]
    [Range(0, 4f)]
    private float frontCheckDistance = 2f;
    private Vector2 forwardCheckDirection = Vector2.right;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        RaycastHit2D groundCheckRay = Physics2D.Raycast(ledgeCheckPoint.position, Vector2.down, ledgeCheckDistance);
        RaycastHit2D forwardCheckRay = Physics2D.Raycast(ledgeCheckPoint.position, forwardCheckDirection, frontCheckDistance);
        Debug.DrawRay(ledgeCheckPoint.position, Vector2.down, Color.red);
        Debug.DrawRay(ledgeCheckPoint.position, Vector2.right, Color.red);

        if (forwardCheckRay.collider)
        {
            ChangeDirection();
        }
        else if (!groundCheckRay.collider)
        {
            ChangeDirection();
        }


        lastOnGroundTimer -= Time.deltaTime;
    }

    private void ChangeDirection()
    {
        if (isFacingRight)
        {
            horizontalInput = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
            isFacingRight = false;
            forwardCheckDirection = Vector2.right;
        }
        else
        {
            horizontalInput = -1;
            transform.eulerAngles = new Vector3(0, 180, 0);
            isFacingRight = true;
            forwardCheckDirection = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        UpdateHorizontalMovement();
        UpdateFriction();
        UpdateSpriteDirection();
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
