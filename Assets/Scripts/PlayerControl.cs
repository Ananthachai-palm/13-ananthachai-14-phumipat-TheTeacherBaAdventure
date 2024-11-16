using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Variable
    private float speed = 10.0f;
    private float jumpPower = 5.0f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCol;
    private Vector2 velocity;

    private bool isOnGroud;
    private bool airJump = true;
    // Awake
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }
    
    // Update
    private void Update()
    {
        // Use to collect way to run between 1(right) or -1(left)
        float inputXAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputXAxis * speed, rb.velocity.y);

        Jump();
    }

    private void Jump()
    {
        // Use to check if Player stay on Groud and press Spacebar
        if (isOnGroud && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // Jump
            isOnGroud = false;
        }
        else if (airJump && Input.GetKeyDown(KeyCode.Space)) // Use to check if Player on air and press Spacebar
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // AirJump
            airJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Use to check if Player stand on the Groud
        // When Player on the Groud, Player will reset jump 
        if(collision.gameObject.tag == "Groud")
        {
            isOnGroud = true;
            airJump = true;
        }
    }


}
