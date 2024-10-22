using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Random = UnityEngine.Random;

public class GameResultInfo : MonoBehaviour
{
    public static GameResultInfo Instance { get; private set; }
    public List<string> MedalsGotStr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        
        SceneManager.activeSceneChanged += (arg0, scene) =>
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                print("IF ASD)_");
                AddResults();
            }
        };
        
        
        print("START ASD)_");
        MedalsGotStr = new List<string>();
        
    }

    public int Exp;
    public int BattlePass_Exp;
    public int Money;

    public void AddResults()
    {
        BattlePassManager.Instance.AddExp(BattlePass_Exp);
        ExpManager.Instance.AddExp(Exp);
        
        SpawnCoins(Money);
        
        MenuUIManager.Instance.AddResultsReact();
        print("ADD RES ASD)_");
        Destroy(gameObject);
    }
    
    public void SpawnCoins(int count)
    {
        for(int i = 0; i < count/5; i++)
        {
            GameObject spawnedCoin = Instantiate(MenuUIManager.Instance.coinPrefab, MenuUIManager.Instance.coinSpawnParent);
            spawnedCoin.transform.localPosition = new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-400.0f, -300.0f), -2000f);
        }  
    }

    private void OnApplicationQuit()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
