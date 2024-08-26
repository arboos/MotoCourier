using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBox : MonoBehaviour
{
    [Header("Ref")] 
    public GameObject boxWindow;
    private Transform spawnPoint;

    public void CreateBoxWindow()
    {
        spawnPoint = transform.parent.parent.parent.parent;
        Instantiate(boxWindow, spawnPoint);
    }
}
