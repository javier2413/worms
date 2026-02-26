using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{

    public WormIdentity identity;

    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float jumpForceY = 7f;
    public float jumpForceX = 3f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    private bool isMyTurn = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        identity = GetComponent<WormIdentity>();
    }

    void Update()
    {
        if (!isMyTurn) return;
        CheckGround();
    }

    void FixedUpdate()
    {
        if (!isMyTurn) return;
        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );
    }

    //BOTONES

    public void WalkLeft()
    {
        if (!isMyTurn) return;
        moveInput = -1f;
    }

    public void WalkRight()
    {
        if (!isMyTurn) return;
        moveInput = 1f;
    }

    public void StopWalk()
    {
        moveInput = 0f;
    }

    public void JumpLeft()
    {
        if (!isMyTurn || !isGrounded) return;

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-jumpForceX, jumpForceY), ForceMode2D.Impulse);
    }

    public void JumpRight()
    {
        if (!isMyTurn || !isGrounded) return;

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
    }

    public void SetTurn(bool value)
    {
        isMyTurn = value;
        moveInput = 0f;

        if (!value)
            rb.velocity = Vector2.zero;
    }
}
