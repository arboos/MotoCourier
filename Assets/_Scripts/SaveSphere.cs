using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SaveSphere : MonoBehaviour
{
    public GameObject mesh;

    private void Start()
    {
        mesh = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!PlayerInfo.Instance.immunity) return;
        if (other.CompareTag("Cop"))
        {
            other.GetComponent<NavMeshCar>().DestroyCar();
        }
    }

    public async void Play()
    {
        mesh.SetActive(true);
        mesh.transform.DORotate(new Vector3(720f, 720f, 720f), 3f);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        mesh.SetActive(false);
    }
}
