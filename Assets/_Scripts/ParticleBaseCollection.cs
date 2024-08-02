using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBaseCollection : MonoBehaviour
{
    public static ParticleBaseCollection Instance { get; private set; }

    public ParticleSystem explosionCop_Particle;
    public ParticleSystem explosionCopDamage;
    
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
}
