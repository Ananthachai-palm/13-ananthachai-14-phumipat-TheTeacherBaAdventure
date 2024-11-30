using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // Field
    private string _name;
    public string Name
    {
        get
        {
            return _name;
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
