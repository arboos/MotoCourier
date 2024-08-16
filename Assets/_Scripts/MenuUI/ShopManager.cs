using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Structure")] 
    public int offersAmount = 2;
    public int carsAmount = 3;

    [Header("Prefabs")] 
    public GameObject offerPrefab;
    public GameObject giftPrefab;
    public GameObject wheelPrefab;
    public GameObject capsulePrefab;
    public GameObject carPrefab;
    public GameObject[] resourcesPrefabs;

    [Header("UI")] 
    public Transform spawnPoint;

    [Header("References")] public UnownedCarList unownedCarList;
    
    private void Start()
    {
        for (int i = 0; i < offersAmount; i++)
        {
            Instantiate(offerPrefab, spawnPoint);
        }

        Instantiate(giftPrefab, spawnPoint);
        Instantiate(wheelPrefab, spawnPoint);
        Instantiate(capsulePrefab, spawnPoint);

        // Get the number of unowned cars available
        int availableUnownedCars = unownedCarList.GetUnownedCars().Count;

        // Ensure carsAmount doesn't exceed the available unowned cars or the maximum of 5
        carsAmount = Mathf.Min(carsAmount, availableUnownedCars, 5);

        List<CarData> carData = unownedCarList.GetNonRepeatingUnownedCars(carsAmount); 
        
        for (int i = 0; i < carsAmount; i++)
        {
            GameObject carInstance = Instantiate(carPrefab, spawnPoint);
            carInstance.GetComponent<SetUpItem>().carData = Resources.Load<CarData>("Cars/" + carData[i].name.Replace(" ", ""));
        }

        for (int i = 0; i < resourcesPrefabs.Length; i++)
        {
            Instantiate(resourcesPrefabs[i], spawnPoint);
        }
    }
}
