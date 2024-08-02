using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsBaseCollection : MonoBehaviour
{
    public static SoundsBaseCollection Instance { get; private set; }

    [Header("Used from Prefabs")]
    public GameObject Coin_Collected;
    
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
}
