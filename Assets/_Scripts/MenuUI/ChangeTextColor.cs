using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ChangeTextColor : MonoBehaviour
{
    [SerializeField] private float changeSpeed;
    private TextMeshProUGUI text;
    private Tween colorChangeTween;
    
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ChangeColor();
    }

    public async void ChangeColor()
    {
        colorChangeTween = text.DOColor(Color.red, 0.1f);
        await colorChangeTween.ToUniTask();
        for (int i = 0; i < 1000; i++)
        {
            colorChangeTween = text.DOColor(Color.yellow, changeSpeed * 2);
            await colorChangeTween.ToUniTask();

            colorChangeTween = text.DOColor(Color.green, changeSpeed);
            await colorChangeTween.ToUniTask();

            colorChangeTween = text.DOColor(Color.cyan, changeSpeed);
            await colorChangeTween.ToUniTask();

            colorChangeTween = text.DOColor(Color.blue, changeSpeed);
            await colorChangeTween.ToUniTask();

            colorChangeTween = text.DOColor(Color.magenta, changeSpeed);
            await colorChangeTween.ToUniTask();
            
            colorChangeTween = text.DOColor(Color.red, changeSpeed);
            await colorChangeTween.ToUniTask();
        }
    }
}
