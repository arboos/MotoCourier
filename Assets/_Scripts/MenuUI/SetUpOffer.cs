using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetUpOffer : MonoBehaviour
{
    [Header("UI")] 
    public TextMeshProUGUI carNameText;
    public Transform carSpawnPointTransform;
    //public TextMeshProUGUI rarityText;
    //public Sprite[] raritiesImages;
    //public Image rarityImage;

    [Header("Preview")]
    public GameObject ItemPreview;

    [Header("Car Data")]
    public CarData carData;

    private void Start()
    {
        // Setting up item in shop
        carNameText.text = carData.carName;
        GameObject carInstance = Instantiate(carData.carPrefab, carSpawnPointTransform);
        carInstance.GetComponent<PrometeoCarController>().enabled = false;
        //rarityText.text = carData.rarity;
        //rarityImage.sprite = GetRarityImage(carData.rarity);
    }

    public void ShowItem()
    {
        Transform previewSpawnPoint = transform.parent.parent.parent.parent;
        GameObject offerPreview = Instantiate(ItemPreview, previewSpawnPoint);
        offerPreview.GetComponent<SetUpPreview>().carData = carData;
        offerPreview.GetComponent<SetUpPreview>().itemInShop = this.gameObject;
    }

    // private Sprite GetRarityImage(string rarity)
    // {
    //     switch (rarity)
    //     {
    //         case "Rare":
    //             return raritiesImages[0];
    //         case "Epic":
    //             return raritiesImages[1];
    //         case "Mythic":
    //             return raritiesImages[2];
    //         case "Legendary":
    //             return raritiesImages[3];
    //     }
    //
    //     return raritiesImages[3];
    // }
}
