using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : Enemy, ICloseAttack
{
    [field: SerializeField] public float DelayAttackTime { get; set; }
    public float AttackTimer { get; set; }
    public bool IsAttacking { get; set; }
    public Character Target { get; set; }

    public AudioClip DinosaurSound;

    // Awake
    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        Timer();

        Movement();
        IsDead();
    }
    // OnTriggerStay
    private void OnTriggerStay2D(Collider2D collision)
    {
        Character obj = collision.GetComponent<Character>();
        Debug.Log(obj);
        if (obj is Player)
        {
            IsAttacking = true;
            if (IsReadyAttack())
            {
                Target = obj.GetComponent<Player>();
                Attack();
                ResetTimer();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Character obj = collision.GetComponent<Character>();
        if (obj is Player)
        {
            Debug.Log("End");

            Target = null;
            IsAttacking = false;
        }
    }

    // Attack
    public override void Attack()
    {
        _audioSource.PlayOneShot(DinosaurSound);
        CloseAttack(Target);
    }

    // Movement
    public override void Movement()
    {
        if (IsAttacking)
        {
            StopMove();
        }
        else
        {
            base.Movement();
            Animator.SetTrigger("isWalk");
        }
    }
    public void StopMove()
    {
        _rb.velocity = Vector2.zero;
    }
    public void CloseAttack(Character target)
    {
        target.TakeDamage(Damage);
    }
    public void ResetTimer()
    {
        AttackTimer = DelayAttackTime;
    }
    public void Timer()
    {
        AttackTimer -= Time.deltaTime;
    }
    public bool IsReadyAttack()
    {
        return AttackTimer <= 0f;
    }
}
