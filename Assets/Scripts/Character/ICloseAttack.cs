using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICloseAttack
{
    float DelayAttackTime { get; set; }
    float AttackTimer { get; set; }
    bool IsAttacking { get; set; }
    Character Target { get; set; }
    void CloseAttack(Character target);

    void StopMove();
    //_rb.velocity = Vector2.zero;

    void ResetTimer();
    // AttackTimer = DelayAttackTime;

    void Timer();
    // AttackTimer -= Time.deltaTime;

    bool IsReadyAttack();
    // return BulletTimer <= 0f;
}
