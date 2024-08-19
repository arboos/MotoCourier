using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CivilianCarManager : MonoBehaviour
{
    public static CivilianCarManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject[] bonusPrefabs; 
        
    public Transform pointsParent;
    
    
    [HideInInspector] public List<Vector3> destinationPoint;

    public List<GameObject> spawnedBonuses;

    
    
    private void Start()
    {
        spawnedBonuses = new List<GameObject>();
        destinationPoint = new List<Vector3>();
        for (int i = 0; i < pointsParent.childCount; i++)
        {
            destinationPoint.Add(pointsParent.GetChild(i).position);
        }
        
        SpawnBonus();
        SpawnBonus();
        SpawnBonus();
        SpawnBonus();
    }

    /// <summary>
    /// Returns a random point from destinationPoint's list
    /// </summary>
    public Vector3 GetRandomPoint()
    {
        return destinationPoint[Random.Range(0, destinationPoint.Count)];
    }
    
    /// <summary>
    /// Returns a random point from destinationPoint's list
    /// </summary>
    /// <param name="excludePoint - point to include in search"></param>
    public Vector3 GetRandomPoint(Vector3 excludePoint)
    {
        Vector3 pointToReturn;
        
        do
        {
            pointToReturn = destinationPoint[Random.Range(0, destinationPoint.Count)];
        } while (pointToReturn == excludePoint);

        return pointToReturn;
    }
    
    public GameObject GetRandomBonus()
    {
        return bonusPrefabs[Random.Range(0, bonusPrefabs.Length)];
    }

    private void SpawnBonus()
    {
        GameObject spawnedBonus = Instantiate(GetRandomBonus());
        spawnedBonus.transform.position = GetRandomPoint();
        spawnedBonus.GetComponent<CivilianCar>().Initialize();
        spawnedBonuses.Add(spawnedBonus);
    }
    
    private void SpawnBonus(GameObject bonusPrefab)
    {
        GameObject spawnedBonus = Instantiate(bonusPrefab);
        spawnedBonus.transform.position = GetRandomPoint();
        spawnedBonus.GetComponent<CivilianCar>().Initialize();
        spawnedBonuses.Add(spawnedBonus);
    }
}
