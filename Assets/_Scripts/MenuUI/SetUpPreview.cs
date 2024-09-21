using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SetUpPreview : MonoBehaviour
{
    [Header("UI")] 
    public TextMeshProUGUI carName;
    public Transform carSpawnPoint;
    public TextMeshProUGUI carCostText;
    public TextMeshProUGUI carText;

    [Header("Car Data")] 
    public CarData carData;

    [HideInInspector] public GameObject itemInShop;

    public static Action OnBuyItem;

    private void Start()
    {
        ShopManager.OnShopUpdate += DestroyItself;
        
        carName.text = carData.carName;
        GameObject carInstance = Instantiate(carData.carPrefab, carSpawnPoint);
        carInstance.GetComponent<PrometeoCarController>().enabled = false;
        carCostText.text = carData.cost.ToString();
    }
    
    public void BuyItem()
    {
        if (YandexGame.savesData.money >= carData.cost)
        {
            YandexGame.savesData.money -= carData.cost;
            YandexGame.savesData.SelectedCarName = carData.carName;
            YandexGame.savesData.AddCar(carData.carName);
            YandexGame.savesData.carsInShop.Remove(carData.carName.Replace(" ", "")); 
            YandexGame.SaveProgress();

            Debug.Log("Car selected and saved (bought): " + carData.carName);
            
            OnBuyItem?.Invoke();

            // Deleting this object
            DestroyItself();
            
            // Deleting item in shop
            Destroy(itemInShop);
        }
        else
        {
            Debug.Log("Not enough money to buy!");
        }
    }

    public void DestroyItself()
    {
        ShopManager.OnShopUpdate -= DestroyItself;
        
        Destroy(this.gameObject);
    }
    private void OnEnable()
    {
        carText.text = LocalizationManager.Instance.GetLocalizedValue("car");
    }
}
