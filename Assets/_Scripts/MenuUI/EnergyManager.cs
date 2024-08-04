using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance { get; private set; }
    
    [Header("UI")] public TextMeshProUGUI energyText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("One more EnergyManager by name" + gameObject.name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        energyText.text = $"{YandexGame.savesData.energy.ToString()}/10";
    }

    public void RefillEnergy()
    {
        YandexGame.RewVideoShow(1);
    }

    public void Award()
    {
        print("Refilling Energy");
        YandexGame.savesData.energy = 10;
        energyText.text = $"{YandexGame.savesData.energy.ToString()}/10";;
    }
}