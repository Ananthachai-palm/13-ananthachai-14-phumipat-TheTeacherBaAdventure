using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charator : MonoBehaviour
{
    // Field and Propoty
    private int _maxHP;
    private int _hP;
    public int HP
    {
        get
        {
            return _hP;
        }
        set
        {
            Mathf.Clamp(value, 0, _maxHP);
            _hP = value;
        }
    }

    private int _damage;
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    // Init
    public void Init(int inputHP, int inputDamage)
    {
        _hP = inputHP;
        _maxHP = inputHP;

        _damage = inputDamage;
    }
    
    // Method
    public void TakeDamage(int inputDamage)
    {
        _hP = inputDamage;
    }

    public bool IsDead()
    {
        return _hP <= 0; 
    }
}
