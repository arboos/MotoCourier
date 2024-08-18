using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Structure")] 
    public int offersAmount = 2;
    public int carsAmount = 3;

    [Header("Update time")] 
    public float refreshTimeInHours = 24f;

    [Header("Prefabs")] 
    public GameObject offerPrefab;
    public GameObject giftPrefab;
    public GameObject wheelPrefab;
    public GameObject capsulePrefab;
    public GameObject carPrefab;
    public GameObject[] resourcesPrefabs;

    [Header("UI")] 
    public Transform spawnPoint;
    public TextMeshProUGUI countdownText;

    [Header("References")] 
    public UnownedCarList unownedCarList;
    public ScrollToItem scrollToItem;

    private DateTime lastUpdateTime;

    private void OnEnable()
    {
        LoadLastUpdateTime();
        StartCoroutine(ShopRefreshRoutine());
        StartCoroutine(UpdateCountdownRoutine());
    }

    private IEnumerator ShopRefreshRoutine()
    {
        while (true)
        {
            // Calculate time since the last update
            TimeSpan timeSinceLastUpdate = DateTime.Now - lastUpdateTime;
            if (timeSinceLastUpdate.TotalHours >= refreshTimeInHours)
            {
                CreateShop();
                SaveLastUpdateTime();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator UpdateCountdownRoutine()
    {
        while (true)
        {
            TimeSpan timeSinceLastUpdate = DateTime.Now - lastUpdateTime;
            TimeSpan timeUntilNextUpdate = TimeSpan.FromHours(refreshTimeInHours) - timeSinceLastUpdate;

            if (timeUntilNextUpdate.TotalSeconds > 0)
            {
                countdownText.text = $"{timeUntilNextUpdate.Hours:D2}:{timeUntilNextUpdate.Minutes:D2}:{timeUntilNextUpdate.Seconds:D2}";
            }
            else
            {
                countdownText.text = "Shop is updating...";
            }

            yield return new WaitForSeconds(1f);
        }
    }
    
    public void CreateShop()
    {
        // Clear the old shop items before refreshing
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }

        // Clear the saved cars list to avoid duplication
        YandexGame.savesData.carsInShop.Clear();

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

            // Save the car's name to the shop save data
            YandexGame.savesData.carsInShop.Add(carData[i].name);
        }

        for (int i = 0; i < resourcesPrefabs.Length; i++)
        {
            Instantiate(resourcesPrefabs[i], spawnPoint);
        }

        // Save the shop state
        YandexGame.SaveProgress();
        
        // Set up correct view 
        Invoke(nameof(MoveToStartOfShop), 0.05f);
    }

    public void LoadSavedShop()
    {
        // Clear the old shop items before refreshing
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < offersAmount; i++)
        {
            Instantiate(offerPrefab, spawnPoint);
        }

        Instantiate(giftPrefab, spawnPoint);
        Instantiate(wheelPrefab, spawnPoint);
        Instantiate(capsulePrefab, spawnPoint);

        for (int i = 0; i < YandexGame.savesData.carsInShop.Count; i++)
        {
            GameObject carInstance = Instantiate(carPrefab, spawnPoint);
            carInstance.GetComponent<SetUpItem>().carData = Resources.Load<CarData>("Cars/" + YandexGame.savesData.carsInShop[i].Replace(" ", ""));
        }

        for (int i = 0; i < resourcesPrefabs.Length; i++)
        {
            Instantiate(resourcesPrefabs[i], spawnPoint);
        }
        
        Invoke(nameof(MoveToStartOfShop), 0.05f);
    }

    // DATA MANAGEMENT

    private void SaveLastUpdateTime()
    {
        lastUpdateTime = DateTime.Now;
        YandexGame.savesData.lastShopUpdateTime = lastUpdateTime.ToString();
        YandexGame.SaveProgress();
    }

    private void LoadLastUpdateTime()
    {
        if (!string.IsNullOrEmpty(YandexGame.savesData.lastShopUpdateTime))
        {
            lastUpdateTime = DateTime.Parse(YandexGame.savesData.lastShopUpdateTime);
            LoadSavedShop();
        }
        else
        {
            // If it's the first time, set the last update time to now
            lastUpdateTime = DateTime.Now;
            SaveLastUpdateTime();
            CreateShop();
        }
    }

    private void MoveToStartOfShop()
    {
        scrollToItem.OnClickScrollTo("OffersTemplate(Clone)");
    }
}
