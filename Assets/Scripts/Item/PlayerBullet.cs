using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Item
{
    // Awake
    private void Awake()
    {
        Init("Mugen");
    }
    public override void UseItem(Player player)
    {
        // When use item. it's will reduce amount items by 1 
        player.PlayerBullet.AmountItem--;

        // Instantiate mugen
        GameObject obj = Instantiate(player.Bullet, player.BulletSpawnPoint.transform.position, Quaternion.identity);
        Broom broom = obj.GetComponent<Broom>();
        broom.Init(player.Damage, player.GetComponent<IShootable>());

        // Destroy after DestroyTime is finish
        Destroy(obj, broom.DestroyTime);

        // Debug
        Debug.Log($"Player Shoot {Name}");
    }
}
