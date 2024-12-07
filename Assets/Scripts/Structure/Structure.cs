using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public abstract void OnHitWith(Character inputChar);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHitWith(collision.GetComponent<Character>());
    }
}
