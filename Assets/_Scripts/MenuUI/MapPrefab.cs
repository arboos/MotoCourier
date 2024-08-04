using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MapPrefab : MonoBehaviour
{
    [Header("Game Map")]
    public GameMap gameMap;

    [Header("References")] 
    public Image image;
    public TextMeshProUGUI textMeshProUGUI;
    
    void Start()
    {
        image.sprite = gameMap.mapImage;
        textMeshProUGUI.text = gameMap.mapName;
    }

    public void SetGameScene()
    {
        int energy = YandexGame.savesData.energy;
        
        if (energy > 0)
        {
            YandexGame.savesData.energy -= 1;
            YandexGame.SaveProgress();
            SceneManager.LoadScene(gameMap.mapName);
        }
        else Debug.Log($"Not enough energy to proceed. Current amount of energy: {energy}");
    }
}
