using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // Field and Propoty
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }

    private int _amountItem;
    public int AmountItem
    {
        get
        {
            return _amountItem;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            _amountItem = value;
        }
    }

    // Abstract
    public abstract void UseItem(Player player);

    // Init
    public void Init(string inputName)
    {
        _name = inputName;
    }

    // Destroy
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
