using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCarData : MonoBehaviour
{
    [Header("Data path")]
    [SerializeField] private string resourceFilePath;

    public void LoadData()
    {
        CarData carData = Resources.Load<CarData>(resourceFilePath + PlayerPrefs.GetString("SelectedCarName"));
        Debug.Log(carData.name);
    }
}
