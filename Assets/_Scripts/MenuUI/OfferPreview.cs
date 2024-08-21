using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;
using System;

public class OfferPreview : MonoBehaviour
{
    [Header("UI")] 
    public TextMeshProUGUI carName;
    public Transform carSpawnPoint;
    public TextMeshProUGUI previousCostText;
    public TextMeshProUGUI currentCostText;

    [Header("Car Data")] 
    public CarData carData;

    [HideInInspector] public GameObject itemInShop;

    public static Action OnBuyOffer;

    private ShopManager shopManager;

    private void Start()
    {
        ShopManager.OnShopUpdate += DestroyItself;
        
        carName.text = carData.carName;
        GameObject carInstance = Instantiate(carData.carPrefab, carSpawnPoint);
        carInstance.GetComponent<PrometeoCarController>().enabled = false;
        currentCostText.text = (carData.cost / 2).ToString();
        previousCostText.text = carData.cost.ToString();
        shopManager = FindObjectOfType<ShopManager>();
    }
    
    public void BuyItem()
    {
        if (YandexGame.savesData.money >= carData.cost / 2)
        {
            YandexGame.savesData.money -= carData.cost / 2;
            YandexGame.savesData.SelectedCarName = carData.carName;
            YandexGame.savesData.AddCar(carData.carName);
            YandexGame.savesData.carsInShop.Remove(carData.carName.Replace(" ", ""));
            YandexGame.savesData.offersAmount -= 1;
            YandexGame.SaveProgress();
        
            Debug.Log("Car selected and saved (bought): " + carData.carName);
        
            OnBuyOffer?.Invoke();

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
}
