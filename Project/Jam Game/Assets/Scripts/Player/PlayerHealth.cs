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

        if (currentHealth <= 0) {
            PlayerDestroyed();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayerDestroyed() { 
        
    }
}
