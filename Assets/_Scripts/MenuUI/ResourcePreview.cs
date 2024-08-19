using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class ResourcePreview : MonoBehaviour
{
    [Header("Data")] 
    public int cost;
    public int amount;
    public string type;
    public Sprite icon;
    public Image mainIcon;

    [Header("UI")] 
    public TextMeshProUGUI costText;
    public TextMeshProUGUI amountText;
    
    public static Action OnBuyReresources;

    private void Start()
    {
        ShopManager.OnShopUpdate += DestroyItself;
        
        // Setting up resource preview
        costText.text = cost.ToString();
        amountText.text = amount.ToString();
        mainIcon.sprite = icon;
    }

    public void Buy()
    {
        switch (type)
        {
            case "Coins":
                if (YandexGame.savesData.gems >= cost)
                {
                    YandexGame.savesData.gems -= cost;
                    YandexGame.savesData.money += amount;
                    Debug.Log($"Buying coins: amount {amount}, cost {cost}");
                    OnBuyReresources?.Invoke();
                    DestroyItself();
                } 
                break;
            //case BuyResources.ItemType.Gems:
        }
        
    }

    public void DestroyItself()
    {
        ShopManager.OnShopUpdate -= DestroyItself;
        
        Destroy(this.gameObject);
    }
}
