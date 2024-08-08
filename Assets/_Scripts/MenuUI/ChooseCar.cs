using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using YG;

public class ChooseCar : MonoBehaviour
{
    [Header("Data")]
    public CarData[] carDataArray; 
    private GameObject currentCarInstance;
    private int currentIndex = 0;

    [Header("UI")]
    public TextMeshProUGUI carNameText;
    public Transform spawnPoint;
    public TextMeshProUGUI carDescriptionText;
    public TextMeshProUGUI carRarity;
    public TextMeshProUGUI selectText;
    public GameObject shopUI;
    public GameObject lobbyUI;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        UpdateMoneyText();
        string savedCarName = YandexGame.savesData.SelectedCarName;
        if (string.IsNullOrEmpty(savedCarName)) {
            savedCarName = carDataArray[0].carName;
        }
        currentIndex = GetCarIndexByName(savedCarName);
        UpdateCarSelection();
    }

    public void NextCar()
    {
        Destroy(currentCarInstance);

        currentIndex++;
        if (currentIndex >= carDataArray.Length)
        {
            currentIndex = 0;
        }

        UpdateCarSelection();
    }

    public void PreviousCar()
    {
        Destroy(currentCarInstance);

        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = carDataArray.Length - 1;
        }

        UpdateCarSelection();
    }

    void UpdateCarSelection()
    {
        if (carDataArray.Length == 0) return;

        CarData carData = carDataArray[currentIndex];
        currentCarInstance = Instantiate(carData.carPrefab, spawnPoint);
        currentCarInstance.transform.localScale = new Vector3(250, 250, 250);
        currentCarInstance.GetComponent<PrometeoCarController>().enabled = false;
        carNameText.text = carData.carName;
        carDescriptionText.text = carData.carDescription;
        carRarity.text = carData.rarity;

        bool hasCar = YandexGame.savesData.HasCar(carData.carName);
        selectText.text = hasCar ? (YandexGame.savesData.SelectedCarName == carData.carName ? "Equipped" : "Equip") : carData.cost.ToString();
    }

    public void SelectCar()
    {
        CarData carData = carDataArray[currentIndex];
        
        if (YandexGame.savesData.HasCar(carData.carName))
        {
            YandexGame.savesData.SelectedCarName = carData.carName;
            YandexGame.SaveProgress();

            selectText.text = "Equipped";
            shopUI.SetActive(false);
            lobbyUI.SetActive(true);

            Debug.Log("Car selected and saved (was in inventory): " + carData.carName);
        }
        else
        {
            if (YandexGame.savesData.money >= carData.cost)
            {
                YandexGame.savesData.money -= carData.cost;
                YandexGame.savesData.SelectedCarName = carData.carName;
                YandexGame.savesData.AddCar(carData.carName);
                YandexGame.SaveProgress();

                selectText.text = "Equipped";
                shopUI.SetActive(false);
                lobbyUI.SetActive(true);

                Debug.Log("Car selected and saved (bought): " + carData.carName);
                UpdateMoneyText();
            }
            else
            {
                Debug.Log("Not enough money to buy!");
            }
        }
    }

    public void Back()
    {
        CarData currentCarData = carDataArray[currentIndex];
        string selectedCarName = YandexGame.savesData.SelectedCarName;

        if (YandexGame.savesData.HasCar(currentCarData.carName) && currentCarData.carName == selectedCarName)
        {
            Debug.Log("Current car is already selected and in the inventory: " + currentCarData.carName);
        }
        else
        {
            Destroy(currentCarInstance);
            int savedCarIndex = GetCarIndexByName(selectedCarName);

            if (currentIndex != savedCarIndex)
            {
                currentIndex = savedCarIndex;
                UpdateCarSelection();
                Debug.Log("Switched to the last saved car: " + carDataArray[savedCarIndex].carName);
            }
        }
    }

    private int GetCarIndexByName(string carName)
    {
        for (int i = 0; i < carDataArray.Length; i++)
        {
            if (carDataArray[i].carName == carName)
            {
                return i;
            }
        }

        return 0;
    }

    public void UpdateMoneyText()
    {
        moneyText.text = YandexGame.savesData.money.ToString();
    }

    private void OnEnable()
    {
        this.transform.rotation = Quaternion.Euler(6, 180, 0);
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
        }
        GUILayout.Label("By clicking this button you will reset data of obtained cars (IN-GAME ONLY)", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Label("Made for testing CarCapsule drop!", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Space(10);
        
        if (GUILayout.Button("Add 1000$"))
        {
            YandexGame.savesData.money += 1000;
            YandexGame.SaveProgress();
            ChooseCar chooseCar = (ChooseCar)target;
            chooseCar.UpdateMoneyText();
            Debug.Log($"Money after add: {YandexGame.savesData.money}");
        }
        GUILayout.Label("By clicking this button you will add to player's \n current balance 1000$ (IN-GAME ONLY)", EditorStyles.centeredGreyMiniLabel);
    }
}
