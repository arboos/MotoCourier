using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    private Animator meshAnimator;
    private bool spawned;

    private void Start()
    {
        meshAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        StartCoroutine(StartAnimation());
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger enter by: " + other.gameObject.name);
        if (other.CompareTag("PlayerDamagable") && spawned)
        {
            spawned = false;
            StartCoroutine(Refresh());
        }
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(Random.Range(0.01f, 1.00f));
        meshAnimator.SetBool("Start", true);
        spawned = true;
    }
    
    private IEnumerator Refresh()
    {
        //ADD COINS TO MANAGER
        meshAnimator.SetTrigger("Collect");
        GameResultInfo.Instance.Money += 25;
        UIManager.OnAddCoins.Invoke();
        GameObject coinAudio = Instantiate(SoundsBaseCollection.Instance.Coin_Collected);
        coinAudio.transform.position = transform.position;
        coinAudio.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.6f);
        

        meshAnimator.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(20f);

        meshAnimator.gameObject.SetActive(true);
        meshAnimator.SetBool("Start", true);
        spawned = true;
    }
}
