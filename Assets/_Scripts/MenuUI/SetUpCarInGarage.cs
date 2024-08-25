using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        carNameText.text = carData.carName;
        GameObject carInstance = Instantiate(carData.carPrefab, spawnPoint);
        carInstance.GetComponent<PrometeoCarController>().enabled = false;
        
        rarityText.text = carData.rarity;
        rarityImage.sprite = GetRarityImage(carData.rarity);
    }

    public void OpenCar()
    {
        
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
