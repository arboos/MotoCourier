using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapDisplayer : MonoBehaviour
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
        SceneManager.LoadScene(gameMap.mapName);
    }
}
