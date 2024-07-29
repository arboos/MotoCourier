using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCar : MonoBehaviour
{

    // Update is called once per frame
    void Awake()
    {
        StartCoroutine(MoveTo());
    }

    private IEnumerator MoveTo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GetComponent<NavMeshAgent>().destination = GameManager.Instance.player.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(0);
        if (other.CompareTag("PlayerFront"))
        {
            print(3);
            DestroyCar();
        }
    }

    private void DestroyCar()
    {
        print(1);
        ParticleSystem tempParticles = Instantiate(ParticleBaseCollection.Instance.explosionCop_Particle);
        tempParticles.gameObject.transform.position = transform.position + Vector3.up*2;
        tempParticles.Play();
        Destroy(gameObject);
    }
}
