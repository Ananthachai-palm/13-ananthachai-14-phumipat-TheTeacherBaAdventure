using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Structure
{
    [SerializeField] private float _jumpPower;
    private Rigidbody2D _rb;

    public override void OnHitWith(Character inputChar)
    {
        if (inputChar is Player)
        {
            _rb = inputChar.GetComponent<Rigidbody2D>();
            _rb.velocity = Vector2.up * _jumpPower;

            Debug.Log($"Trampoline : {_rb}");
        }
    }
}
