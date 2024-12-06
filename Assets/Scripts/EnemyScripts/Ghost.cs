using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ghost : Enemy, IShootable
{
    [SerializeField] private Transform[] _flyPoint;
    [SerializeField] private float _flyPower;

    [field: SerializeField] public Transform BulletSpawnPoint { get; set; }
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public float CoolDownTime { get; set; }
    public float BulletTimer { get; set; }

    // Awake
    private void Awake()
    {
        Init();
    }

    // Update
    private void Update()
    {
        Timer();

        Movement();
        IsDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HitBox " + collision);
        Character obj = collision.GetComponent<Character>();
        if (obj is Player)
        {
            Attack();
        }
    }

    public override void Movement()
    {
        Fly();
        base.Movement();
    }

    public override void Attack()
    {
        Shoot();
    }

    public void Fly()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _flyPower);

        if (_rb.position.y <= _flyPoint[0].position.y && _flyPower < 0) // if fly down
        {
            FlySwitch();
        }
        else if (_rb.position.y >= _flyPoint[1].position.y && _flyPower > 0) // if fly up
        {
            FlySwitch();
        }
    }
    public void FlySwitch()
    {
        _flyPower *= -1f;
    }

    public void Shoot()
    {
        if (IsReadyToShoot())
        {
            _animator.SetTrigger("isAttack");
            GameObject obj = Instantiate(Bullet, BulletSpawnPoint.transform.position, Quaternion.identity);
            Puke puke = obj.GetComponent<Puke>();
            puke.Init(Damage, GetComponent<IShootable>());
            Destroy(obj, puke.DestroyTime);
            ResetTimer();
            _animator.SetTrigger("isIdle");
        }
    }
    public void Timer()
    {
        BulletTimer -= Time.deltaTime;
    }
    public void ResetTimer()
    {
        BulletTimer = CoolDownTime;
    }
    public bool IsReadyToShoot()
    {
        return BulletTimer <= 0f;
    }
}