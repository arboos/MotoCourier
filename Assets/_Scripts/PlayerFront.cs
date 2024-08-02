using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFront : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cop"))
        {
            print("COP DESTROY!");
            other.gameObject.GetComponent<NavMeshCar>().DestroyCar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cop"))
        {
            print("COP DESTROY!");
            other.gameObject.GetComponent<NavMeshCar>().DestroyCar();
        }
    }
}
