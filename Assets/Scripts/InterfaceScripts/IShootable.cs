using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    Transform BulletSpawnPoint { get; set; }
    GameObject Bullet { get; set; }
    float CoolDownTime { get; set; }
    float BulletTimer { get; set; }

    void Shoot();

    void ResetTimer();
    // BulletTimer = CoolDownTime;

    void Timer();
    // BulletTimer -= Time.deltaTime;

    bool IsReadyToShoot();
    // return BulletTimer <= 0f;
}
