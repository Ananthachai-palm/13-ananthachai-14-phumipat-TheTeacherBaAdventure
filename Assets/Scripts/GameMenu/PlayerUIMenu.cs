using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIMenu : MonoBehaviour
{
    public TextMeshProUGUI LifeTxt;
    public TextMeshProUGUI HealPotionTxt;
    public TextMeshProUGUI BroomTxt;

    public Player Player;

    private void Start()
    {
        LifeTxt.text = Player.Life.ToString();
        HealPotionTxt.text = Player.Potion.AmountItem.ToString();
        BroomTxt.text = Player.PlayerBullet.AmountItem.ToString();
    }

    public void UpdateText(TextMeshProUGUI textUI, string setText)
    {
        textUI.text = setText;
    }
}
