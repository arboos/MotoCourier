using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class ReceiveGift : MonoBehaviour
{
    [Header("UI")] public TextMeshProUGUI amountText;

    [Header("Data")] 
    [Range(1, 1000)] public int giveMaxCoins;
    [Range(1, 1000)] public int giveMaxGems;
    [Range(1, 1000)] public int giveMaxEnergy;

    private int amountToGive;

    public static Action OnReceiveGift;

    private void Start()
    {
        switch (gameObject.name)
        {
            case "GotCoinGift(Clone)":
                amountToGive = Random.Range(1, giveMaxCoins);
                YandexGame.savesData.money += amountToGive;
                break;
            case "GotGemGift(Clone)":
                amountToGive = Random.Range(1, giveMaxGems);
                YandexGame.savesData.gems += amountToGive;
                break;
            case "GotEnergyGift(Clone)":
                amountToGive = Random.Range(1, giveMaxEnergy);
                YandexGame.savesData.energy += amountToGive;
                break;
        }
        
        amountText.text = amountToGive.ToString();
        
        YandexGame.SaveProgress();
        
        OnReceiveGift?.Invoke();
    }

    public void DestroyItself()
    {
        YandexGame.savesData.gotGiftToday = true;
        YandexGame.SaveProgress();
        
        Destroy(this.gameObject);
    }
}
