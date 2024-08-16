using System.Collections.Generic;
using UnityEngine;
using YG;

public class UnownedCarList : MonoBehaviour
{
    [Header("Data")]
    public CarContainer carContainer; // Reference to CarContainer

    // Get a specified number of non-repeating cars
    public List<CarData> GetNonRepeatingUnownedCars(int numberOfCars)
    {
        List<CarData> unownedCars = GetUnownedCars(); // Get all unowned cars
        List<CarData> nonRepeatingCars = new List<CarData>();

        // Shuffle and select the required number of cars
        for (int i = 0; i < numberOfCars && unownedCars.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, unownedCars.Count);
            nonRepeatingCars.Add(unownedCars[randomIndex]);
            unownedCars.RemoveAt(randomIndex); // Remove selected car to avoid repetition
        }

        return nonRepeatingCars;
    }

    // Get the full list of unowned cars
    public List<CarData> GetUnownedCars()
    {
        List<CarData> unownedCars = new List<CarData>();

        foreach (var carData in carContainer.carDataArray)
        {
            if (!YandexGame.savesData.HasCar(carData.carName))
            {
                unownedCars.Add(carData);
            }
        }

        return unownedCars;
    }

    // Debug method to print the list of unowned cars
    public void DebugPrintUnownedCars()
    {
        List<CarData> unownedCars = GetNonRepeatingUnownedCars(5);
        foreach (var car in unownedCars)
        {
            Debug.Log("Unowned Car: " + car.carName);
        }
    }
}