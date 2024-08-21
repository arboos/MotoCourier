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
    public static Action OnShopUpdate;

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
    
    // ----------------------------- CREATE -----------------------------

    public void CreateShop()
    {
        // Clear the old shop items before refreshing
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }

        // Clear the saved cars list to avoid duplication
        YandexGame.savesData.carsInShop.Clear();
        
        // Load saved offersAmount or keep the current one if already set
        YandexGame.savesData.offersAmount = 2;
        YandexGame.SaveProgress();

        offersAmount = YandexGame.savesData.offersAmount;

        Instantiate(giftPrefab, spawnPoint);
        Instantiate(wheelPrefab, spawnPoint);
        Instantiate(capsulePrefab, spawnPoint);
        
        int availableUnownedCars = unownedCarList.GetUnownedCars().Count;  // Get the number of unowned cars available
        carsAmount = Mathf.Min(carsAmount, availableUnownedCars, 7); // Ensure carsAmount doesn't exceed the available unowned cars or the maximum of 7
        if (carsAmount > 0)
        {
            List<CarData> carData = unownedCarList.GetNonRepeatingUnownedCars(carsAmount);
            
            // Spawn the first 'offersAmount' cars in offers, if there are enough cars
            for (int i = 0; i < Math.Min(offersAmount, carsAmount); i++)
            {
                GameObject offerInstance = Instantiate(offerPrefab, spawnPoint);
                offerInstance.GetComponent<SetUpOffer>().carData = Resources.Load<CarData>("Cars/" + carData[i].name.Replace(" ", ""));
                
                // Save the car's name to the shop save data
                YandexGame.savesData.carsInShop.Add(carData[i].name);
            }

            // Spawn the remaining cars as regular shop items, if there are any left
            for (int i = offersAmount; i < carsAmount; i++)
            {
                GameObject carInstance = Instantiate(carPrefab, spawnPoint);
                carInstance.GetComponent<SetUpItem>().carData = Resources.Load<CarData>("Cars/" + carData[i].name.Replace(" ", ""));
            
                // Save the car's name to the shop save data
                YandexGame.savesData.carsInShop.Add(carData[i].name);
            }
        }
        else
        {
            Debug.Log("No unowned cars available to display.");
        }

        foreach (GameObject resourcesPrefab in resourcesPrefabs)
        {
            Instantiate(resourcesPrefab, spawnPoint);
        }

        // Save the shop state
        YandexGame.SaveProgress();
        
        // Set up correct view 
        Invoke(nameof(MoveToStartOfShop), 0.05f);
        
        OnShopUpdate?.Invoke();

        YandexGame.savesData.gotGiftToday = false;
        YandexGame.SaveProgress();
    }

    
    // ----------------------------- LOAD -----------------------------  

    public void LoadSavedShop()
    {
        // Clear the old shop items before refreshing
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }
        
        offersAmount = YandexGame.savesData.offersAmount;
    
        // Load other items
        if (!YandexGame.savesData.gotGiftToday) { Instantiate(giftPrefab, spawnPoint); }
        Instantiate(wheelPrefab, spawnPoint);
        Instantiate(capsulePrefab, spawnPoint);

        List<string> savedCarsInShop = YandexGame.savesData.carsInShop;

        if (savedCarsInShop.Count > 0)
        {
            // Spawn the first 'offersAmount' cars in offers, if there are enough cars
            for (int i = 0; i < Math.Min(offersAmount, savedCarsInShop.Count); i++)
            {
                GameObject offerInstance = Instantiate(offerPrefab, spawnPoint);
                offerInstance.GetComponent<SetUpOffer>().carData = Resources.Load<CarData>("Cars/" + savedCarsInShop[i].Replace(" ", ""));
            }

            // Spawn the remaining cars as regular shop items, if there are any left
            for (int i = offersAmount; i < savedCarsInShop.Count; i++)
            {
                GameObject carInstance = Instantiate(carPrefab, spawnPoint);
                carInstance.GetComponent<SetUpItem>().carData = Resources.Load<CarData>("Cars/" + savedCarsInShop[i].Replace(" ", ""));
            }
        }
        else
        {
            Debug.Log("No saved cars available to load.");
        }
    
        // Load resources
        foreach (GameObject resourcesPrefab in resourcesPrefabs)
        {
            Instantiate(resourcesPrefab, spawnPoint);
        }

        // Set the correct view
        Invoke(nameof(MoveToStartOfShop), 0.05f);
    }



    // ----------------------------- DATA MANAGEMENT -----------------------------

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
