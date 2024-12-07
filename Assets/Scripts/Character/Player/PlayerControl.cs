using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    // Variable
    private float _speed = 7.5f;
    private float _jumpPower = 6.25f;
    [SerializeField]
    private float _range = 1f;

    private bool _isJump;
    private bool _isAirJump;

    private Player _player;

    private Rigidbody2D _rb;
    private RaycastHit2D _hit;

    // Awake
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }
    
    // Update
    private void Update()
    {
        // Use to collect way to run between 1(right) or -1(left)
        float inputXAxis = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(inputXAxis * _speed, _rb.velocity.y);
        if (inputXAxis > 0.01f)
        {
            transform.localScale = Vector3.one;
            _player.Animator.SetTrigger("isWalk");
        }
        else if (inputXAxis < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _player.Animator.SetTrigger("isWalk");
        }
        else if (inputXAxis == 0f)
        {
            _player.Animator.SetTrigger("isIdle");
        }
        // Use check jump from Player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChackJump();
            IsPlayerJump();
        }
        Debug.DrawRay(transform.position, Vector2.down * _range, Color.red);

    }

    private void ChackJump()
    {
        _hit = Physics2D.Raycast(transform.position, Vector2.down, _range);
        // Use to check if Player stand on the Groud
        // When Player on the Groud, Player will reset jump 
        if (_hit.collider != null)
        {
            if (_hit.collider.CompareTag("Groud") || _hit.collider.GetComponent<Enemy>() is not null)
            {
                _isJump = true;
                _isAirJump = true;
            }
        }
    }

    // Use when Player click Spacbar to Jump
    private void IsPlayerJump()
    {
        // Use to check if Player stay on Groud and press Spacebar
        if (_isJump)
        {
            Debug.Log("isJump");

            Jump(); // Jump
            _isJump = false;
            _player.Animator.SetTrigger("isLand");
        }
        else if (_isAirJump) // Use to check if Player on air and press Spacebar
        {
            Debug.Log("isAirJump");
            Jump(); // AirJump
            _isAirJump = false;
            _player.Animator.SetTrigger("isLand");
        }
    }

    private void Jump()
    {
        _player.Animator.SetTrigger("isJump");
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower); // Jump
    }
}
