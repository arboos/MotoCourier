using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GiveGift : MonoBehaviour
{
    [Header("UI")] 
    public GameObject[] gifts;

    public void GiveGiftToPlayer()
    {
        YandexGame.savesData.gotGiftToday = true;
        
        
        Transform giftSpawnPoint = transform.parent.parent.parent.parent;
        Instantiate(gifts[Random.Range(0, gifts.Length)], giftSpawnPoint);
        
        Destroy(this.gameObject);
    }
}
