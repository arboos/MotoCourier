using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceCar : CivilianCar
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDamagable"))
        {
            if(PlayerInfo.Instance.currentHealth < 3) PlayerInfo.Instance.TakeHealth(1);
            DestroyCar();
        }
    }
}
