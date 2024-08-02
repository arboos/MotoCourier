using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseCar : MonoBehaviour
{
    [Header("Data")]
    public CarData[] carDataArray; 
    private GameObject currentCarInstance;
    private int currentIndex = 0; 
    private const string SelectedCarKey = "SelectedCarName";

    [Header("UI")]
    public TextMeshProUGUI carNameText;
    public Transform spawnPoint;
    public TextMeshProUGUI carDescriptionText;
    public TextMeshProUGUI carRarity;
    public TextMeshProUGUI selectText;
    public GameObject shopUI;
    public GameObject lobbyUI;

    void Start()
    {
        string savedCarName = PlayerPrefs.GetString(SelectedCarKey, carDataArray[0].carName);
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

        selectText.text = SavingManager.instance.GetBool($"Has{carData.carName}") ? PlayerPrefs.GetString(SelectedCarKey) == carData.carName ? "Equipped" : "Equip" : carData.cost.ToString(); //"Select"
    }

    public void SelectCar()
    {
        // Getting car info
        CarData carData = carDataArray[currentIndex];
        
        
        
        // Checking if has such skin
        if (SavingManager.instance.GetBool($"Has{carData.carName}"))
        {
            PlayerPrefs.SetString(SelectedCarKey, carData.carName);
            PlayerPrefs.Save();

            selectText.text = "Equipped";
            shopUI.SetActive(false);
            lobbyUI.SetActive(true);

            Debug.Log("Car selected and saved (was in inventory): " + carData.carName);
        }
        else
        {
            // Checking if enough money 
            if (SavingManager.instance.GetMoney() >= carData.cost)
            {
                SavingManager.instance.SetMoney(SavingManager.instance.GetMoney() - carData.cost);
                PlayerPrefs.SetString(SelectedCarKey, carData.carName);
                SavingManager.instance.SetBool($"Has{carData.carName}", true);
                PlayerPrefs.Save();
                
                selectText.text = "Equipped";
                
                shopUI.SetActive(false);
                lobbyUI.SetActive(true);

                Debug.Log("Car selected and saved (bought): " + carData.carName);
            }
            else
            {
                Debug.Log($"Not enough money to buy!");
            }
        }
    }

    public void Back()
    {
        CarData currentCarData = carDataArray[currentIndex];
        string selectedCarName = PlayerPrefs.GetString(SelectedCarKey, carDataArray[0].carName);

        // Проверка наличия текущего автомобиля и был ли он выбран
        if (SavingManager.instance.GetBool($"Has{currentCarData.carName}") && currentCarData.carName == selectedCarName)
        {
            Debug.Log("Current car is already selected and in the inventory: " + currentCarData.carName);
        }
        else
        {
            // Если текущий автомобиль не выбран или отсутствует, восстановить последний сохраненный автомобиль
            Destroy(currentCarInstance);
            int savedCarIndex = GetCarIndexByName(selectedCarName);

            // Проверка, чтобы не дублировать действия, если сохраненный автомобиль уже выбран
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

    private void OnEnable()
    {
        this.transform.rotation = Quaternion.Euler(6, 180, 0);
    }
}