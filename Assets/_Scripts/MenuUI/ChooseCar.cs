using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using YG;

public class ChooseCar : MonoBehaviour
{
    [Header("Data")]
    public CarContainer carContainer; 
    private GameObject currentCarInstance;
    private int currentIndex = 0;
    private List<CarData> ownedCars = new List<CarData>();

    [Header("UI")]
    public TextMeshProUGUI carNameText;
    public Transform spawnPoint;
    public TextMeshProUGUI carDescriptionText;
    public TextMeshProUGUI carRarity;
    public TextMeshProUGUI selectText;
    public GameObject shopUI;
    public GameObject lobbyUI;
    public Sprite[] raritiesImages;
    public Image rarityImage;

    void OnEnable()
    {
        PopulateOwnedCars();

        if (YandexGame.savesData.previewCarInGarage == "")
        {
            if (ownedCars.Count == 0)
            {
                // If no cars are owned, assign the first car in CarContainer as the default
                var defaultCar = carContainer.carDataArray[0];
                YandexGame.savesData.AddCar(defaultCar.carName);
                YandexGame.savesData.SelectedCarName = defaultCar.carName;
                YandexGame.SaveProgress();

                PopulateOwnedCars();
            }

            string savedCarName = YandexGame.savesData.SelectedCarName;
            currentIndex = GetCarIndexByName(savedCarName);
        }
        else
        {
            currentIndex = GetCarIndexByName(YandexGame.savesData.previewCarInGarage);
        }
        
        UpdateCarSelection();
    }

    private void OnDisable()
    {
        Destroy(currentCarInstance);
    }
    
    void PopulateOwnedCars()
    {
        ownedCars.Clear();
        foreach (var carData in carContainer.carDataArray)
        {
            if (YandexGame.savesData.HasCar(carData.carName))
            {
                ownedCars.Add(carData);
            }
        }
    }

    void UpdateCarSelection()
    {
        if (currentCarInstance != null)
        {
            Destroy(currentCarInstance);
        }

        CarData carData = carContainer.carDataArray[currentIndex];
        bool isCarOwned = YandexGame.savesData.HasCar(carData.carName);
        
        currentCarInstance = Instantiate(carData.carPrefab, spawnPoint);
        currentCarInstance.transform.localScale = new Vector3(currentCarInstance.transform.localScale.x * 200, currentCarInstance.transform.localScale.x * 200, currentCarInstance.transform.localScale.z * 200);
        currentCarInstance.GetComponent<PrometeoCarController>().enabled = false;
        carNameText.text = carData.carName;
        carDescriptionText.text = carData.carDescription;
        carRarity.text = carData.rarity;
        rarityImage.sprite = GetRarityImage(carData.rarity);
        selectText.text = isCarOwned ? YandexGame.savesData.SelectedCarName == carData.carName ? "Equipped" : "Equip" : $"{carData.cost}";
    }
    
    public void SelectCar()
    {
        CarData carData = carContainer.carDataArray[currentIndex];

        if (YandexGame.savesData.HasCar(carData.carName))
        {
            EquipCar(carData);
        }
        else
        {
            AttemptPurchaseCar(carData);
        }
    }

    private void EquipCar(CarData carData)
    {
        YandexGame.savesData.SelectedCarName = carData.carName;
        selectText.text = "Equipped";
        shopUI.SetActive(false);
        lobbyUI.SetActive(true);

        YandexGame.savesData.previewCarInGarage = "";
        YandexGame.SaveProgress();

        Debug.Log("Car selected and saved (was in inventory): " + carData.carName);
    }

    private void AttemptPurchaseCar(CarData carData)
    {
        Debug.Log("Car not owned. Initiate purchase process for: " + carData.carName);

        if (YandexGame.savesData.money >= carData.cost) // тут потом можно будет поменять за что машина будет покупаться машина (как по мне лучше за гемы)
        {
            YandexGame.savesData.money -= carData.cost;
            YandexGame.savesData.AddCar(carData.carName);
            ownedCars.Add(carData);
            EquipCar(carData);
            YandexGame.SaveProgress();

            Debug.Log("Car purchased and saved: " + carData.carName);
        }
        else
        {
            Debug.Log("Not enough money to purchase: " + carData.carName);
        }
    }
    
    public void Back()
    {
        CarData currentCarData = carContainer.carDataArray[currentIndex];
        string selectedCarName = YandexGame.savesData.SelectedCarName;

        if (currentCarData.carName != selectedCarName)
        {
            Destroy(currentCarInstance);
            int savedCarIndex = GetCarIndexByName(selectedCarName);

            if (currentIndex != savedCarIndex)
            {
                currentIndex = savedCarIndex;
                UpdateCarSelection();
                Debug.Log("Switched to the last saved car: " + carContainer.carDataArray[savedCarIndex].carName);
            }
        }
        
        Destroy(currentCarInstance);

        this.gameObject.SetActive(false);
        
        YandexGame.savesData.previewCarInGarage = "";
        YandexGame.SaveProgress();
    }

    private int GetCarIndexByName(string carName)
    {
        for (int i = 0; i < carContainer.carDataArray.Length; i++)
        {
            if (carContainer.carDataArray[i].carName == carName)
            {
                return i;
            }
        }

        return 0;
    }
    

    private Sprite GetRarityImage(string rarity)
    {
        switch (rarity)
        {
            case "Rare":
                return raritiesImages[0];
            case "Epic":
                return raritiesImages[1];
            case "Mythic":
                return raritiesImages[2];
            case "Legendary":
                return raritiesImages[3];
        }

        return raritiesImages[3];
    }
}

// -------------------------------- CUSTOM EDITOR --------------------------------

[CustomEditor(typeof(ChooseCar))]
public class ChooseCarEditor : Editor
{
    private int currentTab = 0;
    private string[] tabNames = { "General", "Data Manage" };
    
    public override void OnInspectorGUI()
    {
        currentTab = GUILayout.Toolbar(currentTab, tabNames);

        switch (currentTab)
        {
            case 0:
                DrawGeneralSettings();
                break;
            case 1:
                DrawDataCarManage();
                break;
        }
    }

    private void DrawGeneralSettings()
    {
        GUILayout.Label("General Settings", EditorStyles.boldLabel);
        DrawDefaultInspector();
    }

    private void DrawDataCarManage()
    {
        GUILayout.Label("Data Manage", EditorStyles.boldLabel);
        if (GUILayout.Button("Reset Data"))
        {
            Debug.Log("Data was reset");
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
        GUILayout.Label("By clicking this button you will reset data of obtained cars (IN-GAME ONLY)", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Label("Made for testing CarCapsule drop!", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Space(10);
        
        if (GUILayout.Button("Add 1000$"))
        {
            YandexGame.savesData.money += 1000;
            YandexGame.SaveProgress();
            
            Debug.Log($"Money after add: {YandexGame.savesData.money}");
        }
        GUILayout.Label("By clicking this button you will add to player's \n current balance 1000$ (IN-GAME ONLY)", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Space(10);
        
        if (GUILayout.Button("Add 100 gems"))
        {
            YandexGame.savesData.gems += 100;
            YandexGame.SaveProgress();

            Debug.Log($"Money after add: {YandexGame.savesData.money}");
        }
        GUILayout.Label("By clicking this button you will add to player's \n current balance 100 gems (IN-GAME ONLY)", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Space(10);
    }
}
