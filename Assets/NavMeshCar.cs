using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCar : MonoBehaviour
{
    
    
    void Awake()
    {
        StartCoroutine(MoveTo());
    }

    private IEnumerator MoveTo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GetComponent<NavMeshAgent>().destination = PlayerInfo.Instance.transform.position;
        }
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerFront"))
        {
            DestroyCar();
        }
        
        else if (other.gameObject.CompareTag("PlayerDamagable"))
        {
            DealDamageToPlayer();
            DestroyCar();
        }
    }
    
    
    private void DealDamageToPlayer()
    {
        print("Try to deal damage");
        PlayerInfo.Instance.DealDamage(1);
    }
    
    private void DestroyCar()
    {
        ParticleSystem tempParticles = Instantiate(ParticleBaseCollection.Instance.explosionCop_Particle);
        tempParticles.gameObject.transform.position = transform.position + Vector3.up*2;
        tempParticles.Play();
        Destroy(gameObject);
    }
}
