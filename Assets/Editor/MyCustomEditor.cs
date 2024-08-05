using UnityEngine;
using UnityEditor;
using YG;

public class MyCustomEditor : Editor
{
    [MenuItem("Custom Tools/Delete Data")]
    private static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        YandexGame.ResetSaveProgress();
    }
}