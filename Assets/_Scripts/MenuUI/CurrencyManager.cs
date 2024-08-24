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
    public TextMeshProUGUI energyText;

    private void Start()
    {
        UpdateCurrency();
        ShopManager.OnShopUpdate += UpdateCurrency;
        ResourcePreview.OnBuyReresources += UpdateCurrency;
        ReceiveGift.OnReceiveGift += UpdateCurrency;
        SetUpPreview.OnBuyItem += UpdateCurrency;
        OfferPreview.OnBuyOffer += UpdateCurrency;
        BoxPreview.OnBuyBox += UpdateCurrency;
    }

    private void UpdateCurrency()
    {
        coinsText.text = YandexGame.savesData.money.ToString();
        gemsText.text = YandexGame.savesData.gems.ToString();
        energyText.text = $"{YandexGame.savesData.energy}/10";
    }

    private void OnDisable()
    {
        ShopManager.OnShopUpdate -= UpdateCurrency;
        ResourcePreview.OnBuyReresources -= UpdateCurrency;
        ReceiveGift.OnReceiveGift -= UpdateCurrency;
        SetUpPreview.OnBuyItem -= UpdateCurrency;
        OfferPreview.OnBuyOffer -= UpdateCurrency;
        BoxPreview.OnBuyBox -= UpdateCurrency;
    }
}