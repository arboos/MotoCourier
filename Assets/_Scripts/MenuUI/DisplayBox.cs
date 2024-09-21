using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayBox : MonoBehaviour
{
    [Header("Ref")] 
    public GameObject boxWindow;
    private Transform spawnPoint;
    public TextMeshProUGUI boxText;

    private void OnEnable()
    {
        boxText.text = LocalizationManager.Instance.GetLocalizedValue("box");
    }

    public void CreateBoxWindow()
    {
        spawnPoint = transform.parent.parent.parent.parent;
        Instantiate(boxWindow, spawnPoint);
    }
}
