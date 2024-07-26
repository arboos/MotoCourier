using UnityEngine;
using UnityEditor;

public class MyCustomEditor : Editor
{
    [MenuItem("Custom Tools/Delete Data")]
    private static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}