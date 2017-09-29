using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControler : MonoBehaviour {
    public float speed = 15f;
    public float jumpForce = 1000f;
    public bool jump = false;
    public Transform groundCheck;
    public bool facingLeft = true;
    public bool grounded = false;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.OverlapArea(transform.position, groundCheck.position, LayerMask.NameToLayer("Ground"));

        if(Input.GetAxis("Vertical") > 0 && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate ()
    {
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
