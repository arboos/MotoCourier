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
        YandexGame.savesData.BattlePass_CurrentExp += count;
        YandexGame.SaveProgress(); 
        
        print("1. BP_LVL = " + YandexGame.savesData.BattlePass_Level);
        print("1. BP_CE = " + YandexGame.savesData.BattlePass_CurrentExp);
        
        while(YandexGame.savesData.BattlePass_CurrentExp >= YandexGame.savesData.BattlePass_ExpToNextLevel)
        {
            print("Inside while()");
            YandexGame.savesData.BattlePass_CurrentExp -= YandexGame.savesData.BattlePass_ExpToNextLevel;
            YandexGame.savesData.BattlePass_Level++;
            YandexGame.SaveProgress();
            UpdateExpToNextLevel();
        }
        
        YandexGame.SaveProgress();
        print("2. BP_LVL = " + YandexGame.savesData.BattlePass_Level);
        print("2. BP_CE = " + YandexGame.savesData.BattlePass_CurrentExp);
    }
}
