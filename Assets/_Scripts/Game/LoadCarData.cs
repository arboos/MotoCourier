using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCarData : MonoBehaviour
{
    [Header("Data path")]
    [SerializeField] private string resourceFilePath;
    [HideInInspector] public CarData carData;
    
    private PrometeoCarController prometeoCarController;

    private void Start()
    {
        prometeoCarController = GetComponent<PrometeoCarController>();
        LoadData();
    }

    public void LoadData()
    {
        // Получаем доступ к информации текущей выбранной машины!
        carData = Resources.Load<CarData>(resourceFilePath + PlayerPrefs.GetString("SelectedCarName").Replace(" ", ""));
        
        prometeoCarController.maxSpeed = carData.maxSpeed;
        prometeoCarController.maxReverseSpeed = carData.maxReverseSpeed;
        prometeoCarController.accelerationMultiplier = carData.accelerationMultiplier;
        prometeoCarController.maxSteeringAngle = carData.maxSteeringAngle;
        prometeoCarController.steeringSpeed = carData.steeringSpeed;
        prometeoCarController.brakeForce = carData.brakeForce;
        prometeoCarController.decelerationMultiplier = carData.decelerationMultiplier;
        prometeoCarController.handbrakeDriftMultiplier = carData.handbrakeDriftMultiplier;
        prometeoCarController.bodyMassCenter = carData.bodyMassCenter;
    }
}