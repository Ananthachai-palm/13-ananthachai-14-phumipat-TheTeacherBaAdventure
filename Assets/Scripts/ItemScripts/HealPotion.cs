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
        // When use item. it's will reduce amount items by 1 
        player.Potion.AmountItem--;

        // Heal player HP: 20
        player.HP += 20;

        // Debug
        Debug.Log($"Player use {Name}, Player HP:{player.HP}");
    }
}
