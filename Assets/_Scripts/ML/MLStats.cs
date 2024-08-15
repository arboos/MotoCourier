using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MLStats : MonoBehaviour
{
    public static MLStats Instance { get; private set; }

    public TextMeshProUGUI infoText;
    public float totalP = 0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public int Loses;
    public int Wins;

    private void Start()
    {
        SetStats();
    }

    public async void SetStats()
    {
        for (int i = 0; i < 999999; i++)
        {
            print("SetStats()");
            totalP = ((float)Wins / (float)Loses) * 100f;
            infoText.text = totalP.ToString() + "%";

            Loses = 0;
            Wins = 0;
            
            await UniTask.Delay(TimeSpan.FromSeconds(30f), DelayType.DeltaTime, PlayerLoopTiming.Update);
            

            
        }
    }
}
