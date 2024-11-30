using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : Item
{
    private void Awake()
    {
        Init("Heal Potion");
    }
    public override void UseItem(Player player)
    {
        player.AmoutPotion--;
        player.HP += 20;
        Debug.Log($"Player use {Name}, Player HP:{player.HP}");
    }
}
