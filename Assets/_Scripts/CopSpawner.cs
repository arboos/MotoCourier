using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
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
        Spawner();
    }

    public async UniTask SortPointsToDistance()
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

    public async void Spawner()
    {
        CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        while (true)
        {
            for (int i = 0; i < 999; i++)
            {
                if (copsSpawned.Count < 5)
                {
                    await SortPointsToDistance();
                    
                    GameObject spawned = Instantiate(enemyPrefab);
                    spawned.transform.position = spawnPoints[3].transform.position;
                    copsSpawned.Add(spawned.gameObject);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(spawnCooldown), DelayType.DeltaTime, PlayerLoopTiming.Update,
                    _cancellationToken.Token);
            }
        }
    }
}
