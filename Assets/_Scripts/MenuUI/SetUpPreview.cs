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

    [Header("Car Data")] 
    public CarData carData;

    [HideInInspector] public GameObject itemInShop;

    private void Start()
    {
        ShopManager.OnShopUpdate += DestroyItself;
        
        carName.text = carData.carName;
        GameObject carInstance = Instantiate(carData.carPrefab, carSpawnPoint);
        carInstance.GetComponent<PrometeoCarController>().enabled = false;
    }
    
    public void BuyItem()
    {
        if (YandexGame.savesData.money >= carData.cost)
        {
            YandexGame.savesData.money -= carData.cost;
            YandexGame.savesData.SelectedCarName = carData.carName;
            YandexGame.savesData.AddCar(carData.carName);
            YandexGame.SaveProgress();

            Debug.Log("Car selected and saved (bought): " + carData.carName);

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
        Debug.Log("Destroyed preview object");
    }
}
