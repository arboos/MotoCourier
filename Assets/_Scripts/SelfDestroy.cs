using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timeToDestroy;

    private void Awake()
    {
        StartCoroutine(DestroySelf());
        
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
