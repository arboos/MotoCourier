using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class BuyResources : MonoBehaviour
{
    public enum ItemType
    {
        Coins,
        Gems,
    }

    [Header("Data")]
    public ItemType resourceType;
    public int cost;
    public int amount;

    [Header("UI")] 
    public TextMeshProUGUI costText;
    public TextMeshProUGUI amountText;

    public static Action OnBuyReresources;

    private void Start()
    {
        amountText.text = amount.ToString();
        costText.text = resourceType == ItemType.Coins ? costText.text = cost.ToString() : costText.text = $"{cost.ToString()}$";
    }

    public void BuyRes()
    {
        switch (resourceType)
        {
            case ItemType.Coins:
                if (YandexGame.savesData.gems >= cost)
                {
                    YandexGame.savesData.gems -= cost;
                    YandexGame.savesData.money += amount;
                    Debug.Log($"Buying coins: amount {amount}, cost {cost}");
                    OnBuyReresources?.Invoke();
                }
                break;
            // case ItemType.Gems:
            //     YandexGame.savesData.gems += amount;
            //     break;
        }
        YandexGame.SaveProgress();
    }
}