using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puke : Bullet
{
    private void Start()
    {
        Speed *= GetShootDirection("y");
    }

    public override void Move()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y 
                                            + Speed * Time.deltaTime);
    }
    public override void OnHitWith(Character inputChar)
    {
        // Charactor can hit by this bullet
        if (inputChar is Player)
        {
            inputChar.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
