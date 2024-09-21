using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class BoxPreview : MonoBehaviour
{
    [Header("Data")] 
    public int cost;

    public GameObject boxWindow;

    [Header("UI")] 
    public TextMeshProUGUI costText;
    public TextMeshProUGUI boxText;
    
    public static Action OnBuyBox;

    private void Start()
    {
        ShopManager.OnShopUpdate += DestroyItself;
        costText.text = cost.ToString();
    }

    public void Buy()
    {
        if (YandexGame.savesData.money >= cost)
        {
            YandexGame.savesData.money -= cost;
            Instantiate(boxWindow, transform.parent);
            OnBuyBox?.Invoke();
            
            YandexGame.SaveProgress();
            
            DestroyItself();
        }
    }

    public void DestroyItself()
    {
        ShopManager.OnShopUpdate -= DestroyItself;
        
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        boxText.text = LocalizationManager.Instance.GetLocalizedValue("box");
    }
}
