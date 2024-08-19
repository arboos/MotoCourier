using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class BuyResources : MonoBehaviour
{
    public enum ItemType
    {
        Coins,
        Gems,
    }

    [Header("Data")]
    public ItemType resourceType;
    public int cost;
    public int amount;
    public Sprite icon;

    [Header("UI")] 
    public TextMeshProUGUI costText;
    public TextMeshProUGUI amountText;

    public GameObject coinResourcePreview;
    public GameObject gemResourcePreview;

    private Transform previewSpawnPoint;

    private void Start()
    {
        amountText.text = amount.ToString();
        costText.text = resourceType == ItemType.Coins ? costText.text = cost.ToString() : costText.text = $"{cost.ToString()}$";
        previewSpawnPoint = transform.parent.parent.parent.parent.parent;

    }

    public void BuyRes()
    {
        GameObject resourcePreviewGameObject = null;
        ResourcePreview resourcePreview;

        switch (resourceType)
        {
            case ItemType.Coins:
                resourcePreviewGameObject = Instantiate(coinResourcePreview, previewSpawnPoint);
                break;
            case ItemType.Gems:
                resourcePreviewGameObject = Instantiate(gemResourcePreview, previewSpawnPoint);
                break;
        }

        if (resourcePreviewGameObject != null)
        {
            resourcePreview = resourcePreviewGameObject.GetComponent<ResourcePreview>();
            resourcePreview.type = resourceType.ToString(); // Ensure the type is set correctly
            resourcePreview.cost = cost;
            resourcePreview.amount = amount;
            resourcePreview.icon = icon;
        }

        YandexGame.SaveProgress();
    }
}