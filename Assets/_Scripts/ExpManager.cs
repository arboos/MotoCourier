using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        UpdateExpToNextLevel();
    }

    public void UpdateExpToNextLevel()
    {
        if (YandexGame.savesData.AccountLevel < 5)
        {
            YandexGame.savesData.ExpToNextLevel = 50;
        }
        else if (YandexGame.savesData.AccountLevel >= 5 || YandexGame.savesData.AccountLevel < 10)
        {
            YandexGame.savesData.ExpToNextLevel = 125;
        }
        else if(YandexGame.savesData.AccountLevel >= 10 || YandexGame.savesData.AccountLevel < 20)
        {
            YandexGame.savesData.ExpToNextLevel = 250;
        }
        else if(YandexGame.savesData.AccountLevel >= 20)
        {
            YandexGame.savesData.ExpToNextLevel = 500;
        }
        YandexGame.SaveProgress();
    }
    
    public void AddExp(int count)
    {
        YandexGame.savesData.CurrentExp += count;

        while(YandexGame.savesData.CurrentExp >= YandexGame.savesData.ExpToNextLevel)
        {
            YandexGame.savesData.CurrentExp -= YandexGame.savesData.ExpToNextLevel;
            YandexGame.savesData.AccountLevel++;
            YandexGame.SaveProgress();
            UpdateExpToNextLevel();
        }
    }
}
