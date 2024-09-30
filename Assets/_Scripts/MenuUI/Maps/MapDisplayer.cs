using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapDisplayer : MonoBehaviour
{
    [Header("Game Map")]
    public GameMap[] gameMaps;

    [Header("Prefab")]
    public GameObject gameMapPrefab;
    public Transform content;

    [Header("UI")] public TextMeshProUGUI backText;

    private void Start()
    {
        backText.text = LocalizationManager.Instance.GetLocalizedValue("back");

        foreach (var gameMap in gameMaps)
        {
            GameObject newGameMap = Instantiate(gameMapPrefab, content);
            newGameMap.GetComponent<MapPrefab>().gameMap = gameMap;
        }
    }
}
