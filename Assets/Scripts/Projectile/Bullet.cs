using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    // Field and Propoty
    private IShootable Shooter;
    private int _damamge;
    public int Damage
    {
        get
        {
            return _damamge;
        }
    }
    [SerializeField] private int _speed;
    public int Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }
    [SerializeField] private float _destroyTime;
    public float DestroyTime
    {
        get
        {
            return _destroyTime;
        }
        set
        {
            if (value <= 0f)
            {
                value = 1f;
            }
            _destroyTime = value;
        }
    }

    private void Update()
    {
        Move();
    }

    public abstract void Move();
    public abstract void OnHitWith(Character inputChar);

    public void Init(int inputCharacterDamage, IShootable owner)
    {
        _damamge = inputCharacterDamage;
        Shooter = owner;

        DestroyTime = _destroyTime;
    }
    /// <summary>
    /// Summary:
    ///     Returns the value of direction between X or Y axis up to argument ("X" / "Y"),
    ///     that use to move bullet
    /// </summary>
    public int GetShootDirection(string axis)
    {
        //Debug.Log("GetShootDirection");

        int value = 0;
        if (axis.ToUpper() is "X")
        {
            float shootDirX = Shooter.BulletSpawnPoint.transform.position.x - Shooter.BulletSpawnPoint.transform.parent.position.x;
            if (shootDirX > 0)
            {
                Debug.Log("Right");
                value = 1;
            }
            else
            {
                Debug.Log("Left");
                value = -1;
            }
        }
        else if (axis.ToUpper() is "Y")
        {
            float shootDirY = Shooter.BulletSpawnPoint.transform.position.y - Shooter.BulletSpawnPoint.transform.parent.position.y;
            if (shootDirY > 0)
            {
                Debug.Log("Up");
                value = 1;
            }
            else
            {
                Debug.Log("Down");
                value = -1;
            }
        }
        return value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHitWith(collision.GetComponent<Character>());
    }

}
