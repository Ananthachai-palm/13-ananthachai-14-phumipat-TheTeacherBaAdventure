using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    // Variable
    public Image HealthBar;
    private float MaxHealth;

    public void InitHealthBar(float inputHealth)
    {
        MaxHealth = inputHealth;
    }
    public void ReduceHealthBar(int currentHealth)
    {
        HealthBar.fillAmount = currentHealth / MaxHealth;
    }
}
