using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class CurrencyManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI gemsText;

    private void Start()
    {
        UpdateCurrency();
        ShopManager.OnShopUpdate += UpdateCurrency;
        BuyResources.OnBuyReresources += UpdateCurrency;
        ReceiveGift.OnReceiveGift += UpdateCurrency;
        SetUpPreview.OnBuyItem += UpdateCurrency;
    }

    private void UpdateCurrency()
    {
        coinsText.text = YandexGame.savesData.money.ToString();
        gemsText.text = YandexGame.savesData.gems.ToString();
    }

    private void OnDisable()
    {
        ShopManager.OnShopUpdate -= UpdateCurrency;
        BuyResources.OnBuyReresources -= UpdateCurrency;
        ReceiveGift.OnReceiveGift -= UpdateCurrency;
        SetUpPreview.OnBuyItem -= UpdateCurrency;
    }
}