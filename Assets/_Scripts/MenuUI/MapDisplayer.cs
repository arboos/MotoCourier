using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplayer : MonoBehaviour
{
    [Header("Game Map")]
    public GameMap[] gameMaps;

    [Header("Prefab")]
    public GameObject gameMapPrefab;
    public Transform content;

    private void Start()
    {
        for (int i = 0; i < gameMaps.Length; i++)
        {
            Instantiate(gameMapPrefab, content);
            gameMapPrefab.transform.
        }
    }
}
