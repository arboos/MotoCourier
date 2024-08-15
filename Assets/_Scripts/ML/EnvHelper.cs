using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvHelper : MonoBehaviour
{
    public Transform[] playerSpawnPos;
    public Transform[] copSpawnPos;

    public Transform[] cops;

    public Material groundMat;

    private void Start()
    {
        groundMat = transform.GetChild(3).GetComponent<MeshRenderer>().material;
    }
}
