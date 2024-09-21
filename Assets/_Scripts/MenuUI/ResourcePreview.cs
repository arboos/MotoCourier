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
    public TextMeshProUGUI resourceText;
    
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
                    YandexGame.SaveProgress();
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
    
    private void OnEnable()
    {
        resourceText.text = LocalizationManager.Instance.GetLocalizedValue(type == "Coins"? "coins": "gems");
    }
}
