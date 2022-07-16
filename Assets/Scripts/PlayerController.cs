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
    private float jumpForce = 5;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector3.up * jumpForce;
        }
    }

    private void HorizontalMovement()
    {
        horizontalInput = Input.GetAxis(HorizontalAxisName);
        transform.Translate(Vector3.right * horizontalInput * movementSpeed * Time.deltaTime);
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
        if (isFacingRight == false && horizontalInput > 0)
        {
            Flip();
        }
        else if (isFacingRight == true && horizontalInput < 0)
        {
            Flip();
        }
    }
}
