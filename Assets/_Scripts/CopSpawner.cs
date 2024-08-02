using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopSpawner : MonoBehaviour
{
    public static CopSpawner Instance { get; private set; }

    public float spawnCooldown;
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPointsParent;
    [SerializeField] private List<Transform> spawnPoints;
    
    public List<GameObject> copsSpawned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        copsSpawned = new List<GameObject>();
        spawnPoints = new List<Transform>();
        for (int i = 0; i < spawnPointsParent.childCount; i++)
        {
            spawnPoints.Add(spawnPointsParent.GetChild(i));
        }
        SortPointsToDistance();
        StartCoroutine(Spawner()); 
    }

    public void SortPointsToDistance()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            for (int j = i; j < spawnPoints.Count - 1; j++)
            {
                if (Vector3.Distance(PlayerInfo.Instance.transform.position, spawnPoints[i].position) >
                    Vector3.Distance(PlayerInfo.Instance.transform.position, spawnPoints[j].position))
                {
                    Transform tempI = spawnPoints[i];
                    spawnPoints[i] = spawnPoints[j];
                    spawnPoints[j] = tempI;
                }
            }
        }
    }

    public IEnumerator Spawner()
    {
        while (true)
        {
            for (int i = 0; i < 999; i++)
            {
                if (copsSpawned.Count < 5)
                {
                    GameObject spawned = Instantiate(enemyPrefab);
                    SortPointsToDistance();
                    enemyPrefab.transform.position = spawnPoints[1].position;
                    copsSpawned.Add(spawned.gameObject);
                }

                yield return new WaitForSeconds(spawnCooldown);
            }
        }
    }
}
