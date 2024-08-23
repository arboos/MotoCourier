using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    [Header("Ref")] public GameObject wheelWindow;
    private Transform spawnPoint;

    public void CreateWheelWindow()
    {
        spawnPoint = transform.parent.parent.parent.parent;
        Instantiate(wheelWindow, spawnPoint);
    }
}
