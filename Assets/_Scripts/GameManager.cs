using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject spaveShperePrefab;

    public static Action OnDealDamage;
    public static Action OnTakeHealth;
    
    public int localCoins;
    
    // public void AddCoins(int count)
    // {
    //     localCoins += count;
    //     OnAddCoins?.Invoke();
    //     //UIManager.Instance.CoinAddReact();
    // }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("One more GameManger by name" + gameObject.name);
            Destroy(gameObject);
        }
    }


    
}
