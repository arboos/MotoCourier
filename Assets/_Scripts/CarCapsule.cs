using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CarCapsule : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator carCapsuleAnim;

    [Header("Data")] 
    [SerializeField] private ChooseCar carsInShop;

    [Header("UI")] 
    public Button openButton;

    private void OnEnable()
    {
        CheckAvailableCars();
    }

    public async void OpenCapsule(int cost)
    {
        if (YandexGame.savesData.money >= cost)
        {
            openButton.interactable = false;
            
            // Taking money
            Debug.Log("Buying capsule");
            YandexGame.savesData.money -= cost;
            YandexGame.SaveProgress();
            
            // Animations
            carCapsuleAnim.SetBool("Open", true);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            Debug.Log("Animation delay completed, performing further actions...");
            
            carCapsuleAnim.SetBool("Shake", true);
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            Debug.Log("Animation delay completed, performing further actions...");
            
            carCapsuleAnim.SetBool("GiveReward", true);
            
            // Giving random car which is not in inventory
            List<CarData> availableCars = new List<CarData>();
            
            foreach (CarData car in carsInShop.carDataArray)
            {
                if (!YandexGame.savesData.HasCar(car.carName))
                {
                    availableCars.Add(car);
                }
            }
            
            if (availableCars.Count > 0)
            {
                int randomIndex = Random.Range(0, availableCars.Count);
                CarData chosenCar = availableCars[randomIndex];
                
                YandexGame.savesData.AddCar(chosenCar.carName);
                YandexGame.SaveProgress();

                Debug.Log($"Player received car: {chosenCar.carName}");
            }
            else
            {
                Debug.Log("No new cars available to give.");
            }

            CheckAvailableCars(); // Re-check car availability after opening a capsule
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private void CheckAvailableCars()
    {
        bool hasAvailableCars = false;

        foreach (CarData car in carsInShop.carDataArray)
        {
            if (!YandexGame.savesData.HasCar(car.carName))
            {
                hasAvailableCars = true;
                break;
            }
        }

        openButton.interactable = hasAvailableCars;

        if (!hasAvailableCars)
        {
            Debug.Log("No cars available to win. Disabling the open button.");
        }
    }
}
