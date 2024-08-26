using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LoadCarData : MonoBehaviour
{
    [Header("Data path")]
    [SerializeField] private string resourceFilePath;

    [HideInInspector] public CarData carData;
    
    private PrometeoCarController prometeoCarController;

    [Header("Environment")] public CameraFollow cameraFollow;
    [SerializeField] private Transform spawnPos;
    private void Start()
    {
        // Получаем доступ к информации текущей выбранной машины!
        carData = Resources.Load<CarData>(resourceFilePath + YandexGame.savesData.SelectedCarName.Replace(" ", ""));
        GameObject carInstance = Instantiate(carData.carPrefab);
        carInstance.transform.position = spawnPos.position;
        carInstance.GetComponent<Rigidbody>().useGravity = true;
        PlayerCopTrigger copTrigger = carInstance.AddComponent<PlayerCopTrigger>();
        PlayerInfo playerInfo = carInstance.AddComponent<PlayerInfo>();
        playerInfo.copTrigger = copTrigger;
        
        prometeoCarController = carInstance.GetComponent<PrometeoCarController>();

        prometeoCarController.carEngineSound = SoundsBaseCollection.Instance.CarEngine;
        prometeoCarController.tireScreechSound = SoundsBaseCollection.Instance.TireScreech;
        prometeoCarController.useSounds = true;
        
        prometeoCarController.throttleButton = UIManager.Instance.throttleButton;
        prometeoCarController.reverseButton = UIManager.Instance.breakesButton;
        prometeoCarController.turnLeftButton = UIManager.Instance.turnLeftButton;
        prometeoCarController.turnRightButton = UIManager.Instance.turnRightButton;
        prometeoCarController.handbrakeButton = UIManager.Instance.handbrakeButton;

        
        
        carInstance.gameObject.tag = "PlayerDamagable";
        carInstance.gameObject.layer = 10;
        ChangeChildLayer(carInstance.transform, 10);
        ChangeChildTag(carInstance.transform, "PlayerDamagable");
        
        GameObject saveSphere = Instantiate(GameManager.Instance.spaveShperePrefab, playerInfo.gameObject.transform);
        saveSphere.transform.localPosition = Vector3.zero;
        playerInfo.saveSphere = saveSphere.GetComponent<SaveSphere>();
        
        LoadData();

        // Setting up camera
        cameraFollow.carTransform = carInstance.transform;
        
    }

    private void ChangeChildLayer(Transform parent, int layer)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.layer = layer;
            if (child.childCount > 0)
            {
                ChangeChildLayer(child, layer);
            }
        }
    }
    
    private void ChangeChildTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.tag = tag;
            if (child.childCount > 0)
            {
                ChangeChildTag(child, tag);
            }
        }
    }
    
    public void LoadData()
    {
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
