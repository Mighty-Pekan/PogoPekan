using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpForce = 8f; // RB gravity scale 1
    [SerializeField] float bouncingForce = 5f;

    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if(IsGrounded())
        {
            // Constant bouncing
            rb.velocity = Vector2.up * bouncingForce;

            if (Input.GetKey(KeyCode.Space))
            {
                HandleJump();
            }
            HandleMovement();
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        return raycastHit.collider != null;
    }

    void HandleJump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }
}
