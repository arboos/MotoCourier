using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class BattlePassManager : MonoBehaviour
{
    public static BattlePassManager Instance { get; private set; }
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
        if (YandexGame.savesData.BattlePass_Level < 5)
        {
            YandexGame.savesData.BattlePass_ExpToNextLevel = 50;
        }
        else if (YandexGame.savesData.BattlePass_Level >= 5 || YandexGame.savesData.BattlePass_Level < 10)
        {
            YandexGame.savesData.BattlePass_ExpToNextLevel = 125;
        }
        else if(YandexGame.savesData.BattlePass_Level >= 10 || YandexGame.savesData.BattlePass_Level < 20)
        {
            YandexGame.savesData.BattlePass_ExpToNextLevel = 250;
        }
        else if(YandexGame.savesData.BattlePass_Level >= 20)
        {
            YandexGame.savesData.BattlePass_ExpToNextLevel = 500;
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
