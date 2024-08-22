using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinMenu : MonoBehaviour
{
    private Tween coinMoveTween;
    public void Awake()
    {
        MoveCoin();
    }

    private async void MoveCoin()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0.05f, 0.3f)));
        
        coinMoveTween = transform.DOMove(MenuUIManager.Instance.coinsImage.position, 2f).SetEase(Ease.InBack);
        await coinMoveTween.ToUniTask();
        
        MenuUIManager.Instance.AddMoney(5);
        Destroy(gameObject);
    }
}
