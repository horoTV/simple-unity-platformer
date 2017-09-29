using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControler : MonoBehaviour {
    public float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    } 

    void FixedUpdate ()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(horizontalInput, verticalInput));
    }
}
