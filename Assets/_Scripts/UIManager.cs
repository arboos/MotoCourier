using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Mobile input")]
    public GameObject mobileInput;
    public GameObject throttleButton;
    public GameObject breakesButton;
    public GameObject turnRightButton;
    public GameObject turnLeftButton;
    public GameObject handbrakeButton;

    
    [Header("Active UI")]
    public TextMeshProUGUI coinsText;
    public Transform coinsImage;
    
    public Image healthImage;
    public TextMeshProUGUI wastedCounter;
    public GameObject deathScreen;


    private Tween coinReactionTween;
    
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

    private void OnEnable()
    {
        GameManager.OnAddCoins += CoinAddReact;
    }

    private void OnDisable()
    {
        GameManager.OnAddCoins -= CoinAddReact;
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

    public async void CoinAddReact()
    {
        if (coinReactionTween == null)
        {
            //coinReactionTween = coinsImage.DOPunchScale(new Vector3(1.05f, 1.05f, 1.05f), 0.1f).SetEase(Ease.InOutElastic);
            coinReactionTween = coinsImage.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.1f).SetEase(Ease.InOutElastic);
            await coinReactionTween.ToUniTask();
            coinsText.text = GameManager.Instance.localCoins.ToString();
            coinReactionTween = null;
        }
    }
}
