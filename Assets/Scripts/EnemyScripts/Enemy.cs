using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    [SerializeField] protected float _speed;
    public GameObject _hitBox;

    [SerializeField] protected Transform[] _movePoints;

    // Update
    private void Update()
    {
        Movement();
        IsDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character obj = collision.GetComponent<Character>();
        if (_hitBox != null && obj is Player)
        {
            Attack();
        }
    }
    public virtual void Movement()
    {
        // Use move Obj and HitBox
        _rb.velocity = new Vector2(_speed, _rb.velocity.y);

        // Use to check limit point left and right for flip
        if (_rb.position.x <= _movePoints[0].position.x && _speed < 0) // if on left point && run to left
        {
            Flip();
        }
        else if (_rb.position.x >= _movePoints[1].position.x && _speed > 0) // if on right point && run to right
        {
            Flip();
        }
    }

    public abstract void Attack();

    public void Flip()
    {
        _speed *= -1;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        //CharHealthBar.transform.localScale *= -1;
    }
    public override bool IsDead()
    {
        if (base.IsDead())
        {
            Destroy(gameObject);
        }
        return base.IsDead();
    }
}