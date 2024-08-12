using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class RewardedAwards : MonoBehaviour
{
    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

// Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

// Подписанный метод получения награды
    void Rewarded(int id)
    {
        if (id == 0)
        {
            PlayerInfo.Instance.Respawn();
        }
        
        else if (id == 1)
        {
            EnergyManager.Instance.Award();
        }
             
        YandexGame.SaveProgress();
        
        // Если ID = 2, то выдаём "+оружие".
        // else if (id == 2)
        //     AddWeapon();
    }

// Метод для вызова видео рекламы
    void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }
}
