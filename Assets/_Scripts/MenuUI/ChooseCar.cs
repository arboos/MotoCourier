using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCar : MonoBehaviour
{
    public GameObject[] cars; 
    private int currentIndex = 0; 
    private const string SelectedCarKey = "SelectedCarIndex";

    void Start()
    {
        // Load the saved car index from PlayerPrefs
        currentIndex = PlayerPrefs.GetInt(SelectedCarKey, 0); // Default to 0 if no value is saved
        UpdateCarSelection();
    }

    public void NextCar()
    {
        cars[currentIndex].SetActive(false);
        
        currentIndex++;
        if (currentIndex >= cars.Length)
        {
            currentIndex = 0;
        }
        
        UpdateCarSelection();
    }

    public void PreviousCar()
    {
        cars[currentIndex].SetActive(false);
        
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = cars.Length - 1;
        }
        
        UpdateCarSelection();
    }

    void UpdateCarSelection()
    {
        cars[currentIndex].SetActive(true);
    }

    public void SelectCar()
    {
        // Save the current car index to PlayerPrefs
        PlayerPrefs.SetInt(SelectedCarKey, currentIndex);
        PlayerPrefs.Save();

        Debug.Log("Car selected and saved: " + currentIndex);
    }
}