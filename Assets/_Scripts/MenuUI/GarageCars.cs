using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GarageCars : MonoBehaviour
{
    public CarData[] ownedCars;
    public CarData[] lockedCars;
    public Transform content;
    public GameObject carGaragePrefab;
    public GameObject lockedCarGaragePrefab;
    public TextMeshProUGUI obtainedCarsFromAllText;
    public CarContainer carContainer;

    public UnownedCarList unownedCarList;

    private void OnEnable()
    {
        obtainedCarsFromAllText.text = $"{YandexGame.savesData.ownedCars.Count}/{carContainer.carDataArray.Length} CARS";
        
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        
        var carDataList = new List<CarData>(YandexGame.savesData.ownedCars.Count);
    
        foreach (var carName in YandexGame.savesData.ownedCars)
        {
            var carData = Resources.Load<CarData>("Cars/" + carName.Replace(" ", ""));
            if (carData != null)
            {
                carDataList.Add(carData);
                InstantiateCarInGarage(carGaragePrefab, carData);
            }
        }

        ownedCars = carDataList.ToArray();
    
        // -------------------------- LOCKED CARS --------------------------
    
        var lockedCarsDataList = new List<CarData>();
    
        foreach (var carName in unownedCarList.GetUnownedCars())
        {
            var carData = Resources.Load<CarData>("Cars/" + carName.name.Replace(" ", ""));
            if (carData != null)
            {
                lockedCarsDataList.Add(carData);
                InstantiateCarInGarage(lockedCarGaragePrefab, carData);
            }
        }
    
        lockedCars = lockedCarsDataList.ToArray();
    }

    private void InstantiateCarInGarage(GameObject prefab, CarData carData)
    {
        var carInGarage = Instantiate(prefab, content);
        carInGarage.GetComponent<SetUpCarInGarage>().carData = carData;
    }
}
