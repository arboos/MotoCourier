using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class GiveGift : MonoBehaviour
{
    [Header("UI")] 
    public GameObject[] gifts;
    public TextMeshProUGUI giftText;

    private void OnEnable()
    {
        giftText.text = LocalizationManager.Instance.GetLocalizedValue("gift");
    }

    public void GiveGiftToPlayer()
    {
        YandexGame.savesData.gotGiftToday = true;
        
        
        Transform giftSpawnPoint = transform.parent.parent.parent.parent;
        Instantiate(gifts[Random.Range(0, gifts.Length)], giftSpawnPoint);
        
        Destroy(this.gameObject);
    }
}
