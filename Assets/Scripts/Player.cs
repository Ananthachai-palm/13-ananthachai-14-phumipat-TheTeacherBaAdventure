using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IShootable
{
    // Field and Propoty
    [field: SerializeField] public Transform BulletSpawnPoint { get; set; }
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public float CoolDownTime { get; set; }
    public float BulletTimer { get; set; }

    // For save amount  items
    [HideInInspector] public HealPotion Potion;
    [HideInInspector] public PlayerBullet PlayerBullet;

    // For set amount  of bullet before game start
    [SerializeField] private int _startAmountBullet;

    // For set amount  lift for respawn
    [SerializeField] private int _life;

    // For save last point player got
    private Vector2 _playerSpawnPoint;


    private void Awake()
    {
        Init();

        // Use to set the first spawnpoint
        _playerSpawnPoint = transform.position;

        // Get for save amount  of items
        Potion = GetComponent<HealPotion>();
        PlayerBullet = GetComponent<PlayerBullet>();

        // Init bullet amount  before game start
        PlayerBullet.AmountItem = _startAmountBullet;
    }

    private void Update()
    {
        // count cooldown time
        Timer();
        
        // Use player to shoot
        Shoot();

        // Use to use Heal Potion
        Heal();

        // if Player is dead
        CheckDead();
    }

    // OnTriggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Use for test OnTriggerEnter2D is collect
        Debug.Log("OnTriggerEnter2D is collect");

        // Use for test What is Object in collision
        Item obj = collision.GetComponent<Item>();
        Debug.Log(obj);

        // if Player collided some items
        if (obj != null)
        {
            Debug.Log("obj is not null");

            if (obj.GetComponent<HealPotion>() is HealPotion)
            {
                Debug.Log("Obj is HealPotion");
                GetItem(obj.GetComponent<HealPotion>());
            }
            else if (obj.GetComponent<PlayerBullet>() is PlayerBullet)
            {
                Debug.Log("Obj is PlayerBullet");
                GetItem(obj.GetComponent<PlayerBullet>());
            }
        }

        // Use for Check Win Game
        if (collision.CompareTag("MooDeng"))
        {
            // Use for stop Player when Game is end
            Destroy(GetComponent<PlayerControl>());
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            Debug.Log("WINNNNNNNNNN");
        }
        else if (collision.CompareTag("CheckPoint"))
        {
            _playerSpawnPoint = collision.transform.position;
        }
    }

    // Method

    // GetItem is use to classify each type for collect amount  of each items
    public void GetItem(HealPotion inputItem)
    {
        Potion.AmountItem++;
        Destroy(inputItem);
        Debug.Log($"Player get Heal Potion, Heal Potion now: {Potion.AmountItem} ");
    }
    public void GetItem(PlayerBullet inputItem)
    {
        PlayerBullet.AmountItem++;
        Destroy(inputItem);
        Debug.Log($"Player get Bullet, Bullet now: {PlayerBullet.AmountItem} ");
    }

    // Use for respawn
    public void Spawning()
    {
        HP = MaxHP;
        transform.position = _playerSpawnPoint;
    }
    public void CheckDead()
    {
        if (IsDead())
        {
            // if Player is dead minus 1 lift
            _life--;

            // if Player die more than limit(limit is 3) Game will end
            if (_life < 0)
            {
                Destroy(gameObject);
                Debug.Log("Lose....");
            }
            else // if game not end will respawn
            {
                Spawning();
            }
        }
    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (IsReadyToShoot())
            {
                PlayerBullet.UseItem(this);
                ResetTimer();
            }
            else
            {
                Debug.Log($"{name} have to cooldown or have no more {PlayerBullet.name}");
            }
        }
    }
    public void Heal()
    {
        if (Input.GetKeyDown("e"))
        {
            if (Potion.AmountItem > 0)
            {
                Potion.UseItem(this);
            }
            else
            {
                Debug.Log($"{name} have no more {Potion.name}");
            }
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
        return BulletTimer <= 0f && PlayerBullet.AmountItem > 0;
    }
}

// Use for test spawn system
/*
        if (Input.GetKeyDown("f"))
        {
            TakeDamage(50);
            Debug.Log(HP);
        }
        if (Input.GetKeyDown("r"))
        {
            Debug.Log($"HP now: {HP}");
        }
*/