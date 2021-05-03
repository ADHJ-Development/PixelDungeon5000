using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D capsule;
    [SerializeField] private LayerMask groundLayerMask;
    private Vector2 velocity;
    public float speed;
    public float jumpForce;
    bool facingRight = true;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        capsule = transform.GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded()) rb.velocity = Vector2.up * jumpForce;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(+speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = Vector2.down * (jumpForce * 2);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180f, 0);
    }

    private bool IsGrounded()
    {
        // RaycastHit2D raycastHit2d = Physics2D.BoxCast(capsule.bounds.center, capsule.bounds.size, 0f, Vector2.down * .1f, groundLayerMask);
        // bool grounded = raycastHit2d.collider != null;

        Vector2 position = this.transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.2f;

        RaycastHit2D hit2D = Physics2D.Raycast(position, direction, distance, groundLayerMask);

        bool grounded = hit2D.collider != null;

        Debug.Log(string.Format("IsGrounded {0}", grounded));

        return grounded;

        //return raycastHit2d.collider != null;
    }
}
