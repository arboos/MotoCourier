using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Map")]
public class GameMap : ScriptableObject
{
    public string mapName;
    public Sprite mapImage;
}
