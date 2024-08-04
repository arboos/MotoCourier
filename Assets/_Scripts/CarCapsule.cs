using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CarCapsule : MonoBehaviour
{
    [SerializeField] private Animator carCapsuleAnim;

    public void OpenCapsule(int cost)
    {
        if (YandexGame.savesData.money >= cost)
        {
            Debug.Log("Buying capsule");
            YandexGame.savesData.money -= cost;
            YandexGame.SaveProgress();
            carCapsuleAnim.SetTrigger("Drop");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
