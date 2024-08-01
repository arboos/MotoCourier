using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
    public static SavingManager instance { get; private set; }
    
    [SerializeField] private const string MoneySaveData = "money";
    public int money;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        money = PlayerPrefs.HasKey(MoneySaveData) ? PlayerPrefs.GetInt(MoneySaveData) : 0;
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int amount)
    {
        PlayerPrefs.SetInt(MoneySaveData, amount);
    }
    
    public void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }
    
    public bool GetBool(string key, bool defaultValue = false)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            return defaultValue;
        }
        
        return PlayerPrefs.GetInt(key) == 1;
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
