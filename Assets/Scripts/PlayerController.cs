using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;

    private Animator anim;

    private float movementInputDirection;

    private int amountOfJumpsLeft;

    public float movementSpeed = 10;

    public float jumpForce = 16.0f;

    public Transform groundCheck;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canJump;

    public int amountOfJumps = 1;

    public float groundCheckRadius;

    public LayerMask whatIsGround;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
        Debug.Log(canJump);
        Debug.Log(rb.velocity.y);
    }

    private void CheckIfCanJump() {
        if (isGrounded && rb.velocity.y <= -0.5) {
            amountOfJumpsLeft = amountOfJumps;
        } 

        if (amountOfJumpsLeft <= 0 ) {
            canJump = false;
        } else {
            canJump = true;
        }
    }

    private void CheckInput() {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    private void UpdateAnimations() {
        anim.SetBool("isWalking", isWalking);
    }

    private void ApplyMovement() {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void CheckMovementDirection() {
        if (isFacingRight && movementInputDirection < 0) {
            Flip();
        } else if(!isFacingRight && movementInputDirection > 0) {
            Flip();
        }

        if (rb.velocity.x != 0) {
            isWalking = true;
        } else {
            isWalking = false;
        }
    }

    private void Jump() {
        if (canJump) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }

    }

    private void CheckSurroundings() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
