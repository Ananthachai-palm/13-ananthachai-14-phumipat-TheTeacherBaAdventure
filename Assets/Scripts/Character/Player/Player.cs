using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private int _startAmountPotion;

    // For set amount  lift for respawn
    [SerializeField] private int _life;
    public int Life
    { 
        get { return _life; }
        set { _life = value; }
    }

    // For save last point player got
    private Vector2 _playerSpawnPoint;

    public PlayerUIMenu _playerUIMenu;

    public AudioClip WalkSound;
    public AudioClip PickItemSound;
    public AudioClip ExplosiveSound;
    public AudioClip HealingSound;
    public AudioClip WinGameSound;

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

        Potion.AmountItem = _startAmountPotion;
    }

    private void Update()
    {
        // count cooldown time
        TimerShoot();
        
        // Use player to shoot
        Shoot();

        // Use to use Heal Potion
        Heal();

        // if Player is dead
        CheckDead();

        if (transform.position.y < -50)
        {
            TakeDamage(100);
        }
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
            _audioSource.PlayOneShot(WinGameSound);

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
        _audioSource.PlayOneShot(PickItemSound);
        Potion.AmountItem++;
        _playerUIMenu.UpdateText(_playerUIMenu.HealPotionTxt, Potion.AmountItem.ToString());
        Destroy(inputItem);
        Debug.Log($"Player get Heal Potion, Heal Potion now: {Potion.AmountItem} ");
    }
    public void GetItem(PlayerBullet inputItem)
    {
        _audioSource.PlayOneShot(PickItemSound);
        PlayerBullet.AmountItem++;
        _playerUIMenu.UpdateText(_playerUIMenu.BroomTxt, PlayerBullet.AmountItem.ToString());
        Destroy(inputItem);
        Debug.Log($"Player get Bullet, Bullet now: {PlayerBullet.AmountItem} ");
    }
    // Use for respawn
    public void Spawning()
    {
        HP = MaxHP;
        transform.position = _playerSpawnPoint;
        CharHealthBar.InitHealthBar(HP);
    }
    public void CheckDead()
    {
        if (IsDead())
        {
            // if Player is dead minus 1 lift
            _life--;
            _playerUIMenu.UpdateText(_playerUIMenu.LifeTxt, Life.ToString());

            // if Player die more than limit(limit is 3) Game will end
            if (_life < 0)
            {
                Destroy(gameObject);
                Debug.Log("Lose....");

                SceneManager.LoadScene("MainMenuScene");
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
                _audioSource.PlayOneShot(ExplosiveSound);
                PlayerBullet.UseItem(this);
                _playerUIMenu.UpdateText(_playerUIMenu.BroomTxt, PlayerBullet.AmountItem.ToString());
                ResetTimerShoot();
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
                _audioSource.PlayOneShot(HealingSound);
                Potion.UseItem(this);
                _playerUIMenu.UpdateText(_playerUIMenu.HealPotionTxt, Potion.AmountItem.ToString());
            }
            else
            {
                Debug.Log($"{name} have no more {Potion.name}");
            }
        }
    }
    public void TimerShoot()
    {
        BulletTimer -= Time.deltaTime;
    }
    public void ResetTimerShoot()
    {
        BulletTimer = CoolDownTime;
    }
    public bool IsReadyToShoot()
    {
        return BulletTimer <= 0f && PlayerBullet.AmountItem > 0;
    }
}