using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public void Awake()
    {
        currentHealth = maxHealth;
        PlayerCurrentHealthUpdate(0);
    }

    /// <summary>
    /// update the max health of player Pod
    /// </summary>
    /// <param name="amount">the amount of changes to player Pod's max health</param>
    public void PlayerMaxHealthUpdate(int amount) {
        maxHealth += amount;
        currentHealth += amount;
    }

    /// <summary>
    /// update the current health of player Pod, and check if player Pod is dead
    /// </summary>
    /// <param name="amount">the amount of changes to player Pod's current health</param>
    public void PlayerCurrentHealthUpdate(float amount) {
        currentHealth += amount;
        HUD.Instance.UpdateHealth(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HUD.Instance.UpdateHealth(maxHealth, currentHealth);
            PlayerDestroyed();
        }
        else if(currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayerDestroyed() { 
        
    }
}
