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
        carNameText.text = carData.carName;
    }

    public void SelectCar()
    {
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
}