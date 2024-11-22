using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    // Variable
    private float speed = 10.0f;
    private float jumpPower = 5.0f;

    private bool isJump;
    private bool isAirJump;

    private Rigidbody2D rb;
    RaycastHit2D hit;


    // Awake
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update
    private void Update()
    {
        // Use to collect way to run between 1(right) or -1(left)
        float inputXAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputXAxis * speed, rb.velocity.y);

        // Use check jump from Player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IfPlayerIsJump();
        }
        Debug.DrawRay(transform.position, Vector2.down * 1.4f, Color.red);

    }

    // Use when Player click Spacbar to Jump
    private void IfPlayerIsJump()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1.4f);
        // Use to check if Player stand on the Groud
        // When Player on the Groud, Player will reset jump 
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Groud"))
            {
                Debug.Log("Groud");
                isJump = true;
                isAirJump = true;
            }
        }

        // Use to check if Player stay on Groud and press Spacebar
        if (isJump)
        {
            Debug.Log("isJump");

            Jump(); // Jump
            isJump = false;
        }
        else if (isAirJump) // Use to check if Player on air and press Spacebar
        {
            Debug.Log("isAirJump");
            Jump(); // AirJump
            isAirJump = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower); // Jump
    }


}
