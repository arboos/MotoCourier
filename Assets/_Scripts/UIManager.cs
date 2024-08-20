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
using Vector2 = UnityEngine.Vector2;

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
    
    public Transform healthImage;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI wastedCounter;
    public GameObject deathScreen;


    private Tween coinReactionTween;
    private Tween healthImageScaleReactionTween;
    private Tween healthImagePositionReactionTween;
    
    private Tween healthImagePositionTakeHealthReactionTween;
    public static Action OnAddCoins;
    
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
        OnAddCoins += CoinAddReact;
        GameManager.OnDealDamage += DealDamageReact;
        GameManager.OnTakeHealth += TakeHealthReact;
    }

    private void OnDisable()
    {
        OnAddCoins -= CoinAddReact;
        GameManager.OnDealDamage -= DealDamageReact;
        GameManager.OnTakeHealth -= TakeHealthReact;
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
            coinReactionTween = coinsImage.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.1f).SetEase(Ease.InOutElastic);
            await coinReactionTween.ToUniTask();
            coinsText.text = GameResultInfo.Instance.Money.ToString();
            coinReactionTween = null;
        }
    }

    public async void DealDamageReact()
    {
        if (healthImageScaleReactionTween == null)
        {
            print("DEALDAMAGEREACT");

            healthImageScaleReactionTween = healthImage.DOPunchScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.Linear);
            await healthImageScaleReactionTween.ToUniTask();
            healthImageScaleReactionTween = null;
        }
    }

    public async void TakeHealthReact()
    {
        if (healthImagePositionTakeHealthReactionTween == null)
        {
            print("TAKE HEALTH REACT");
            healthImagePositionTakeHealthReactionTween = healthImage.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 1.5f).SetEase(Ease.InQuad);
            await healthImagePositionTakeHealthReactionTween.ToUniTask();
            healthImagePositionTakeHealthReactionTween = null;
        }
    }
}
