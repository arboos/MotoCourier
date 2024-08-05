using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private async void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerDamagable"))
        {
            rb.isKinematic = false;
            gameObject.layer = 15;
            Vector3 forceToAdd = other.gameObject.transform.forward * 40f + Vector3.up *2f;
            rb.AddForce(forceToAdd, ForceMode.Impulse);

            CancellationTokenSource _cancelerationToken = new CancellationTokenSource();
            
            await UniTask.Delay(TimeSpan.FromSeconds(3f), DelayType.DeltaTime, PlayerLoopTiming.FixedUpdate, _cancelerationToken.Token);

            rb.isKinematic = true;
            
            await UniTask.Delay(TimeSpan.FromSeconds(3f), DelayType.DeltaTime, PlayerLoopTiming.FixedUpdate, _cancelerationToken.Token);
            
            Destroy(gameObject);

        }
    }
}
