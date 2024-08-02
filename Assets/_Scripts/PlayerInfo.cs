using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    public PlayerCopTrigger copTrigger;
    
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
        if (currentHealth <= 0)
        {
            Death();
        }
        switch (currentHealth)
        {
            case 3:
                UIManager.Instance.healthImage.color = Color.green;
                break;
            
            case 2:
                UIManager.Instance.healthImage.color = Color.yellow;
                break;
            
            case 1:
                UIManager.Instance.healthImage.color = Color.red;
                break;
        }
    }

    public void Death()
    {
        UIManager.Instance.deathScreen.SetActive(true);
        GetComponent<PrometeoCarController>().enabled = false;
    }
}
