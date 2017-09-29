using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float speed = 15f;
    public float jumpForce = 1000f;
    public bool jump = false;
    public Transform groundCheck;
    public bool facingLeft = true;
    public bool grounded = false;
    public float groundCheckOffset = 0.001f;

    private Rigidbody2D rb2d;
    private Collider2D coll;

    private int groundLayerMask;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        groundLayerMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // Get Points below the bounding box
        var groundPt1 = coll.bounds.min + new Vector3(0f, -groundCheckOffset);
        var groundPt2 = groundPt1 + new Vector3(coll.bounds.extents.x * 2f, 0f);

        // Check if touching the ground
        grounded = Physics2D.Linecast(groundPt1, groundPt2, groundLayerMask);
        Debug.DrawLine(groundPt1, groundPt2, grounded ? Color.cyan : Color.red);

        var horizontalInput = Input.GetAxis("Horizontal") * speed;

        if (horizontalInput > 0 && facingLeft)
            Flip();
        else if (horizontalInput < 0 & !facingLeft)
            Flip();

        if (jump)
        {
            jump = false;
            rb2d.AddForce(new Vector2(0, jumpForce));
        }

        rb2d.AddForce(new Vector3(horizontalInput, 0));
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
