using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using YG;

public class MasterVolume : MonoBehaviour
{
    [field: SerializeField] private AudioMixerGroup _audioMixerGroup;

    private bool isMusicOn = true;
    [SerializeField] private GameObject MusicON;
    [SerializeField] private GameObject MusicOFF;


    private void Start()
    {
        isMusicOn = YandexGame.savesData.musicState == "false";
        ChangeMusicState();
        MusicON.SetActive(isMusicOn);
        MusicOFF.SetActive(!isMusicOn);
    }

    public void ChangeMusicState()
    {
        if (isMusicOn)
        {
            _audioMixerGroup.audioMixer.SetFloat("MasterVolume", -80);
            isMusicOn = false;
            YandexGame.savesData.musicState = "false";
            YandexGame.SaveProgress();
        }
        else
        {
            _audioMixerGroup.audioMixer.SetFloat("MasterVolume", 0);
            isMusicOn = true;
            YandexGame.savesData.musicState = "true";
            YandexGame.SaveProgress();
        }

        MusicON.SetActive(isMusicOn);
        MusicOFF.SetActive(!isMusicOn);
    }
}