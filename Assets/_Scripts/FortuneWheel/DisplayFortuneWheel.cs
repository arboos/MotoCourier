using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayFortuneWheel : MonoBehaviour
{
    [Header("Ref")] 
    public GameObject wheelWindow;
    private Transform spawnPoint;
    public TextMeshProUGUI fortuneWheelText;
    public TextMeshProUGUI timePerText;

    private void OnEnable()
    {
        fortuneWheelText.text = LocalizationManager.Instance.GetLocalizedValue("fortuneWheel");
        timePerText.text = LocalizationManager.Instance.GetLocalizedValue("1per24hours");
    }

    public void CreateWheelWindow()
    {
        spawnPoint = transform.parent.parent.parent.parent;
        Instantiate(wheelWindow, spawnPoint);
    }
}
