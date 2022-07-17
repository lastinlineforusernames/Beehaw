using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isFacingRight = true;
    private const string HorizontalAxisName = "Horizontal";
    private float horizontalInput;
    [SerializeField]
    private float movementSpeed = 4;
    [SerializeField]
    private float accelerationRate = 1;
    [SerializeField]
    private float jumpForce = 5;
    private Rigidbody2D rigidbody;
    private bool isJumping;

    private Vector3 velocity;
    private Vector3 lastPosition;

    [SerializeField]
    private ProjectileMovement projectile;
    [SerializeField]
    private Transform projectileSpawnPoint;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        // set velocity and store position for next frame
        velocity = (transform.position - lastPosition / Time.deltaTime);
        lastPosition = transform.position;

        // get player input

        // check collision

        // handle horizontal movement

        // jump apex?

        // update gravity scale

        // handle jump

        // move player
        
        HorizontalMovement();
        Jump();
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
        }
    }

        private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rigidbody.velocity = Vector2.up * jumpForce;
        }
    }

    private void HorizontalMovement()
    {
        horizontalInput = Input.GetAxis(HorizontalAxisName);
        transform.Translate(Vector2.right * horizontalInput * movementSpeed * Time.deltaTime);
        ChangeDirection();
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void ChangeDirection()
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
}
