using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpForce = 8f; // RB gravity scale 1
    [SerializeField] float bouncingForce = 5f;
    [SerializeField] float roundForce = 5f;


    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleMovement();

        if (IsGrounded())
        {
            // Constant bouncing
            rb.velocity = transform.up * bouncingForce;
           //rb.AddForce(Vector2.up * bouncingForce, ForceMode2D.Impulse);

            if (Input.GetKey(KeyCode.Space))
            {
                HandleJump();
            }
        }
    }

    private bool IsGrounded()
    {
       RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground")); 

        return raycastHit.collider != null;
    }

    private void OnDrawGizmos()
    {
        // Show BoxCast on the scene
        Gizmos.color = Color.green;
        Gizmos.DrawCube(boxCollider2D.bounds.center, boxCollider2D.bounds.size);
    }

    void HandleJump()
    {
        rb.velocity = Vector2.up * jumpForce;
        //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        //rb.AddForce(Vector2.up, ForceMode2D.Impulse);

        transform.Rotate(new Vector3(0, 0, -inputX * roundForce));
    }
}
