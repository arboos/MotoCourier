using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountData : MonoBehaviour
{
    public static AccountData Instance { get; private set; }
    
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
