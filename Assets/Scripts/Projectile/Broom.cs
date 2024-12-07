using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Bullet
{
    private void Start()
    {
        Speed *= GetShootDirection("x");
    }

    public override void Move()
    {
        transform.position = new Vector2(transform.position.x + Speed
                                           * Time.deltaTime, transform.position.y);
    }
    public override void OnHitWith(Character inputChar)
    {
        // Charactor can hit by this bullet
        if (inputChar is Enemy)
        {
            inputChar.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
