using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class SetUpItem : MonoBehaviour
{
    [Header("UI")] 
    public TextMeshProUGUI carNameText;
    public Transform carSpawnPointTransform;
    public TextMeshProUGUI rarityText;

    //private UnownedCarList unownedCarList;
    public CarData carData;

    private void Start()
    {
        // Getting list with unowned cars
        //unownedCarList = FindObjectOfType<UnownedCarList>();
        //List<CarData> unownedCars = unownedCarList.GetUnownedCars();
        
        // choosing random from it
        //int randomCarIndex = Random.Range(0, unownedCars.Count);
        //carData = unownedCars[randomCarIndex];
        
        // Setting up item in shop
        carNameText.text = carData.carName;
        GameObject carInstance = Instantiate(carData.carPrefab, carSpawnPointTransform);
        carInstance.GetComponent<PrometeoCarController>().enabled = false;
        rarityText.text = carData.rarity;
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

            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Not enough money to buy!");
        }
    }
}
