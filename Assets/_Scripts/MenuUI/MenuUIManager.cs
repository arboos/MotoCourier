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
