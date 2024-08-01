using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }


    public GameObject mobileInput;
    
    public Image healthImage;
    public TextMeshProUGUI wastedCounter;
    
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
        if (YandexGame.Instance.infoYG.playerInfoSimulation.isMobile)
        {
            mobileInput.SetActive(true);
            PlayerInfo.Instance.gameObject.GetComponent<PrometeoCarController>().useTouchControls = true;
            PlayerInfo.Instance.gameObject.GetComponent<PrometeoCarController>().InitializeControlsMobile();
        }
    }
}
