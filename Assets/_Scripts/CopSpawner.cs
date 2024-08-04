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
    [SerializeField] private GameObject testTextPrefab;
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
            GameObject sText = Instantiate(testTextPrefab, spawnPoints[i]);
            sText.transform.position = spawnPointsParent.GetChild(i).transform.position + Vector3.up*2;
        }
        SortPointsToDistance();
        Spawner();
    }

    public async UniTask SortPointsToDistance()
    {
        print("SORT STARTED");
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
        
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[i].GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
        }
        print("SORT ENDED _ " + PlayerInfo.Instance.transform.position);
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
                    print("COP SPAWNED");
                    await SortPointsToDistance();
                    GameObject spawned = Instantiate(enemyPrefab);
                    enemyPrefab.transform.position = spawnPoints[3].position;
                    print(
                        "EnemySpawned at " + spawnPoints[3].gameObject.transform.GetChild(0).GetComponent<TextMeshPro>()
                            .text + " _ " + spawnPoints[3].name);
                    copsSpawned.Add(spawned.gameObject);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(spawnCooldown), DelayType.DeltaTime, PlayerLoopTiming.Update,
                    _cancellationToken.Token);
            }
        }
    }
}
