using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]

public class CarData : ScriptableObject
{
    [Header("Data")] 
    public string carName;
    public GameObject carPrefab;
}
