using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charator
{
    // Field and Propoty
    private int _amoutPotion;
    public int AmoutPotion
    {
        get
        {
            return _amoutPotion;
        }
        set
        {
            _amoutPotion = value;
        }
    }
    private int _amoutBullet;
    public int AmoutBullet
    {
        get
        {
            return _amoutBullet;
        }
        set
        {
            _amoutBullet = value;
        }
    }
    private void Awake()
    {
        Init(150, 20);
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("E");
        }
    }

    // OnTriggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D is collect");
        Debug.Log(collision);

        Item obj = collision.GetComponent<Item>();
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

    // Method
    public void GetItem(HealPotion inputItem)
    {
        _amoutPotion++;
        Destroy(inputItem);
        Debug.Log($"Player get Heal Potion, Heal Potion now: {_amoutPotion} ");
    }
    public void GetItem(PlayerBullet inputItem)
    {
        _amoutBullet++;
        Destroy(inputItem);
        Debug.Log($"Player get Bullet, Bullet now: {_amoutBullet} ");
    }
}
