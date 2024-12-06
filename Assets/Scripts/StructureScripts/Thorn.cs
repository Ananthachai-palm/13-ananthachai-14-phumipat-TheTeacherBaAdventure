using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : Structure
{
    [SerializeField] private int _damamge;

    public override void OnHitWith(Character inputChar)
    {
        if (inputChar is Player)
        {
            inputChar.TakeDamage(_damamge);
        }
    }
}
