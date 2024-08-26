using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCar : MonoBehaviour
{
    public float speed;
    public Vector3 lastPosition;
    
    void Awake()
    {
        StartCoroutine(MoveTo());
    }

    
    void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude, 0.7f /*adjust this number in order to make interpolation quicker or slower*/);
        lastPosition = transform.position;
    }
    
    private IEnumerator MoveTo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            GetComponent<NavMeshAgent>().destination = PlayerInfo.Instance.transform.position;
        }
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerDamagable"))
        {
            if (Mathf.Abs(Mathf.Abs(PlayerInfo.Instance.speed) - Mathf.Abs(speed)) < 0.05f && PlayerInfo.Instance.speed <= 0.05f) return;
            
            if (PlayerInfo.Instance.speed > speed)
            {
                DestroyCar();
            }
            else
            {
                DealDamageToPlayer();
                DestroyCar();
            }
        }
    }
    
    
    private void DealDamageToPlayer()
    {
        print("Try to deal damage");
        PlayerInfo.Instance.DealDamage(1);
    }
    
    public void DestroyCar()
    {
        ParticleSystem tempParticles = Instantiate(ParticleBaseCollection.Instance.explosionCop_Particle);
        tempParticles.gameObject.transform.position = transform.position + Vector3.up*2;
        tempParticles.Play();
        PlayerInfo.Instance.copTrigger.RemoveMissing();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Cop"))
            {
                PlayerInfo.Instance.copTrigger.copsInside.Remove(hitCollider.gameObject);
                CopSpawner.Instance.copsSpawned.Remove(hitCollider.gameObject);
                Destroy(hitCollider.gameObject);
            }
        }
        CopSpawner.Instance.copsSpawned.Remove(gameObject);
        GameObject spawnedSound = Instantiate(SoundsBaseCollection.Instance.Cop_Explosion.gameObject);
        Destroy(spawnedSound, 2f);
        Destroy(gameObject);
    }
    
    public void DestroyWithLight()
    {
        //ParticleSystem tempParticles = Instantiate(ParticleBaseCollection.Instance.explosionCop_Particle);
        //tempParticles.gameObject.transform.position = transform.position + Vector3.up*2;
        //tempParticles.Play();
        PlayerInfo.Instance.copTrigger.RemoveMissing();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Cop"))
            {
                PlayerInfo.Instance.copTrigger.copsInside.Remove(hitCollider.gameObject);
                CopSpawner.Instance.copsSpawned.Remove(hitCollider.gameObject);
                Destroy(hitCollider.gameObject);
            }
        }
        CopSpawner.Instance.copsSpawned.Remove(gameObject);
        Destroy(gameObject);
    }
}
