using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SetUpCarInGarage : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI carNameText;
    public Transform spawnPoint;
    
    public TextMeshProUGUI rarityText;
    public Sprite[] raritiesImages;
    public Image rarityImage;

    [Header("Car Data")]
    public CarData carData;
    private ChooseCar chooseCar;

    private void Start()
    {
        carNameText.text = carData.carName;
        GameObject carInstance = Instantiate(carData.carPrefab, spawnPoint);
        carInstance.GetComponent<PrometeoCarController>().enabled = false;
        
        rarityText.text = LocalizationManager.Instance.GetLocalizedValue(carData.rarity.Replace(" ", "").ToLower());
        rarityImage.sprite = GetRarityImage(carData.rarity);
    }

    public void OpenCar()
    {
        // Открываем окно с полной информацией о машине где ее можно выбрать / посмотреть когда она выбрана

        Transform canvas = transform.parent.parent.parent.parent.parent;
        
        YandexGame.savesData.previewCarInGarage = carData.carName;
        YandexGame.SaveProgress();
        
        canvas.Find("--[NewGarage]--").gameObject.SetActive(false); // выключаем новый гараж
        canvas.Find("--[Garage]--").gameObject.SetActive(true);
        canvas.Find("--[CARS]--").gameObject.SetActive(true);
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
