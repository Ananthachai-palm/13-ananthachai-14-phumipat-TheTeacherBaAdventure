using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // Field and Propoty
    private int _maxHP;
    public int MaxHP
    {
        get { return _maxHP; }
        set
        {
            if (value <= 0)
            {
                value = 1;
            }
            _maxHP = value;
        }
    }
    private int _hP;
    public int HP
    {
        get
        {
            return _hP;
        }
        set
        {
            _hP = Mathf.Clamp(value, 0, _maxHP);
            charHealthBar.ReduceHealthBar(HP);
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
            if (value <= 0)
            {
                value = 1;
            }
            _damage = value;
        }
    }

    [SerializeField] private Animator _animator;
    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }


    [SerializeField] protected AudioSource _audioSource;
    protected Rigidbody2D _rb;

    // For set HP and Damage outside the scripts (Can set on unity)
    /// <summary>
    ///     Summary: if set value equal 0 or less, it will be 1. So please set value more than 0.
    /// </summary>
    [SerializeField] private int _setHP = 1;
    [SerializeField] private int _setDamage = 1;

    [SerializeField] private HealthBarManager charHealthBar;
    public HealthBarManager CharHealthBar
    {
        get
        {
            return charHealthBar;
        }
    }

    // Init
    public void Init()
    {
        MaxHP = _setHP;
        HP = MaxHP;
        charHealthBar.InitHealthBar(MaxHP);

        Damage = _setDamage;

        _rb = GetComponent<Rigidbody2D>();
    }
    
    // Method
    public void TakeDamage(int inputDamage)
    {
        HP -= inputDamage;
    }

    // Use to check Dead
    public virtual bool IsDead()
    {
        return HP == 0; 
    }
}
