using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform player;
    public Transform point;
    public List<Transform> points;


    // private void Start()
    // {
    //     for (int i = 0; i < point.childCount; i++)
    //     {
    //         points.Add(point.GetChild(i));
    //     }
    // }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
}
