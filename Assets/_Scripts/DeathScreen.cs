using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class DeathScreen : MonoBehaviour
{
    public Image timer;

    public float timerTime;
    
    [SerializeField] private Button respawnAds;
    [SerializeField] private Button respawnGem;
    

    private void OnEnable()
    {
        timerTime = 5f;
    }

    private void Update()
    {
        timerTime -= Time.deltaTime;
        if (timerTime <= 0)
        {
            //Add coins to manager
            //Add all what i need
            SceneManager.LoadScene(0);
        }
        else
        {
            timer.fillAmount = timerTime / 5f;
        }
    }

    public void RespawnAds()
    {
        YandexGame.RewVideoShow(0);
    }
    
    public void RespawnGem()
    {
        //-= gems
        Respawn();
    }
    
    public void Respawn()
    {
        
    }
    
    
}
