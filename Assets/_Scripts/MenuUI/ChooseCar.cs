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

    public static Action OnChangeCar;

    void Start()
    {
        string savedCarName = PlayerPrefs.GetString(SelectedCarKey, carDataArray[0].carName);
        currentIndex = GetCarIndexByName(savedCarName);
        UpdateCarSelection();
    }

    public void NextCar()
    {
        OnChangeCar?.Invoke();
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
        OnChangeCar?.Invoke();
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
        carNameText.text = carData.carName;
        carDescriptionText.text = carData.carDescription;
    }

    public void SelectCar()
    {
        OnChangeCar?.Invoke();
        PlayerPrefs.SetString(SelectedCarKey, carDataArray[currentIndex].carName);
        PlayerPrefs.Save();

        Debug.Log("Car selected and saved: " + carDataArray[currentIndex].carName);
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