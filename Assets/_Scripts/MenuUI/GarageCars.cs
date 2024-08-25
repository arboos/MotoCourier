using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GarageCars : MonoBehaviour
{
    public CarData[] ownedCars;
    public Transform content;
    public GameObject carGaragePrefab;
    public TextMeshProUGUI obtainedCarsFromAllText;
    public CarContainer carContainer;

    private void OnEnable()
    {
        obtainedCarsFromAllText.text = $"{YandexGame.savesData.ownedCars.Count}/{carContainer.carDataArray.Length} CARS";
        
        // Initialize a list to hold the owned cars temporarily
        List<CarData> carDataList = new List<CarData>();

        // Load each car that the player owns from the Resources folder
        foreach (var carName in YandexGame.savesData.ownedCars)
        {
            CarData carData = Resources.Load<CarData>("Cars/" + carName.Replace(" ", ""));
            if (carData != null)
            {
                carDataList.Add(carData);
            }
        }

        // Convert the list to an array and assign it to ownedCars
        ownedCars = carDataList.ToArray();
        
        // Уничтожаем машины для того чтоб выставить только имеющиеся сначала
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (CarData ownedCarData in ownedCars)
        {
            GameObject carInGarage = Instantiate(carGaragePrefab, content);
            carInGarage.GetComponent<SetUpCarInGarage>().carData = ownedCarData;
        }
    }
}
