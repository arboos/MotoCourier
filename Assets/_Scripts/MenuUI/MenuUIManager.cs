using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager Instance { get; private set; }
    
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

    public TextMeshProUGUI coinsText;
    public Transform coinsImage;
    
    private Tween coinReactionTween;
    public GameObject coinPrefab;
    public Transform coinSpawnParent;

    public int tempCoins = 0;


    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerIdText;

    [Header("Battle Pass")] 
    public Image BP_Menu_Line;
    public Image BP_Menu_Circle;
    public TextMeshProUGUI BP_Menu_Level;
    public TextMeshProUGUI BP_Menu_Points;

    public Transform BP_LINE_PARENT;
    

    public void AddResultsReact()
    {
        // BattlePassManager.Instance.AddExp(BattlePass_Exp);
        // ExpManager.Instance.AddExp(Exp);
        // YandexGame.savesData.money += Money;

        BP_Menu_Line.fillAmount =
            (float)YandexGame.savesData.BattlePass_CurrentExp / (float)YandexGame.savesData.BattlePass_ExpToNextLevel;
        
        BP_Menu_Points.text = YandexGame.savesData.BattlePass_CurrentExp.ToString() + "/" +
                              YandexGame.savesData.BattlePass_ExpToNextLevel.ToString();
        
        BP_Menu_Level.text = YandexGame.savesData.BattlePass_Level.ToString();
        
        //if(had_untaken_awards) BP_Menu_Circle.color = Color.yellow; else Color.gray;
        
        for (int i = 0; i < YandexGame.savesData.BattlePass_Level; i++)
        {
            BP_LINE_PARENT.GetChild(i).GetChild(1).GetComponent<Image>().fillAmount = 1;
        }
        BP_LINE_PARENT.GetChild(YandexGame.savesData.BattlePass_Level+1).GetChild(1).GetComponent<Image>().fillAmount = BP_Menu_Line.fillAmount;

        
        
        print($"RESULTS_ADDED: " +
              $"\n BP_Level = {YandexGame.savesData.BattlePass_Level} " +
              $"\n BP_CurExp = {YandexGame.savesData.BattlePass_CurrentExp} " +
              $"\n BP_ExpToN = {YandexGame.savesData.BattlePass_ExpToNextLevel}");
    }

    private void Start()
    {
        playerNameText.text = YandexGame.playerName;
        playerIdText.text = YandexGame.playerId;
    }


    public async void AddMoney(int count)
    {
        tempCoins += count;
        if (coinReactionTween == null)
        {
            coinReactionTween = coinsImage.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.1f).SetEase(Ease.InOutElastic);
            await coinReactionTween.ToUniTask();
            coinsText.text = (YandexGame.savesData.money + tempCoins).ToString();
            coinReactionTween = null;
        }
    }
}
