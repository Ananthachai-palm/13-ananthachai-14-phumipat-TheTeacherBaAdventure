using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public abstract void OnHitWith(Character inputChar);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHitWith(collision.GetComponent<Character>());
    }
}
