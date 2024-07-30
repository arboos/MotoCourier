using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int currentHealth;

    public void DealDamage(int damage)
    {
        print("Damage: " + damage);
        currentHealth -= damage;
        UIManager.Instance.livesText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
