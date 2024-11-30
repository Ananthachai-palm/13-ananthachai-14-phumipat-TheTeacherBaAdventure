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
        player.AmoutBullet--;
        // GameObject obj = Instantiate(player.Bullet, player.transform.position, Quaternion.identity);

        Debug.Log($"Player Shoot {Name}");

    }
}
