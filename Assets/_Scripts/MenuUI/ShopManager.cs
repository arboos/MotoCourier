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
        
       
        int availableUnownedCars = unownedCarList.GetUnownedCars().Count;  // Get the number of unowned cars available
        carsAmount = Mathf.Min(carsAmount, availableUnownedCars, 7); // Ensure carsAmount doesn't exceed the available unowned cars or the maximum of 7
        List<CarData> carData = unownedCarList.GetNonRepeatingUnownedCars(carsAmount);
        
        Debug.Log($"Cars amount: {carsAmount}");

        int startCountFrom = offersAmount;
        
        if (carsAmount >= offersAmount + 3) // if in shop at least 7 cars so 2 of them can be in offers
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject offerInstance = Instantiate(offerPrefab, spawnPoint);
                offerInstance.GetComponent<SetUpOffer>().carData = Resources.Load<CarData>("Cars/" + carData[i].name.Replace(" ", ""));
                
                // Save the car's name to the shop save data
                YandexGame.savesData.carsInShop.Add(carData[i].name);
            }
        }
        else
        {
            startCountFrom = 0;
        }

        Instantiate(giftPrefab, spawnPoint);
        Instantiate(wheelPrefab, spawnPoint);
        Instantiate(capsulePrefab, spawnPoint);
        
        for (int i = startCountFrom; i < carsAmount; i++)
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

        List<string> savedCarsInShop = YandexGame.savesData.carsInShop;
        int startCountFrom = offersAmount;

        // Ensure offers are loaded correctly if there are enough saved cars
        if (savedCarsInShop.Count >= offersAmount + 3)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject offerInstance = Instantiate(offerPrefab, spawnPoint);
                offerInstance.GetComponent<SetUpOffer>().carData = Resources.Load<CarData>("Cars/" + savedCarsInShop[i].Replace(" ", ""));
            }
        }
        else
        {
            startCountFrom = 0;
        }

        // Load other items
        if (!YandexGame.savesData.gotGiftToday) { Instantiate(giftPrefab, spawnPoint); }
        Instantiate(wheelPrefab, spawnPoint);
        Instantiate(capsulePrefab, spawnPoint);

        // Load remaining cars in the shop
        for (int i = startCountFrom; i < savedCarsInShop.Count; i++)
        {
            GameObject carInstance = Instantiate(carPrefab, spawnPoint);
            carInstance.GetComponent<SetUpItem>().carData = Resources.Load<CarData>("Cars/" + savedCarsInShop[i].Replace(" ", ""));
        }

        // Load resources
        for (int i = 0; i < resourcesPrefabs.Length; i++)
        {
            Instantiate(resourcesPrefabs[i], spawnPoint);
        }

        // Set the correct view
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
