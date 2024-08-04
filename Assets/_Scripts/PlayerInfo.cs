using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    public PlayerCopTrigger copTrigger;
    public int currentHealth = 3;
    
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
        GetComponent<Rigidbody>().isKinematic = true;
        UIManager.Instance.deathScreen.SetActive(true);
        UIManager.Instance.wastedCounter.gameObject.SetActive(false);
        GetComponent<PrometeoCarController>().enabled = false;
    }
    
    public void Respawn(int hp)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<PrometeoCarController>().enabled = true;
        currentHealth = hp;
        
        copTrigger.wasted = false;
        UIManager.Instance.wastedCounter.gameObject.SetActive(false);
        copTrigger.timeToWaste = 4f;
        copTrigger.RemoveMissing();

        StartCoroutine(DestroyCops());
        
        UIManager.Instance.deathScreen.SetActive(false);
    }

    public IEnumerator DestroyCops()
    {
        yield return new WaitForSeconds(0.5f);
        
        foreach (var cop in CopSpawner.Instance.copsSpawned)
        {
            cop.GetComponent<NavMeshCar>().DestroyCar();
        }
    }
}
