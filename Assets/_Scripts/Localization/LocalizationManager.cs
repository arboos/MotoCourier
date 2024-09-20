using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    private Dictionary<string, Dictionary<string, string>> localizedTexts;
    private string currentLanguage = "ru";  // Язык по умолчанию
    public static Action OnChangeLanguage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalization();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadLocalization()
    {
        TextAsset localizationFile = Resources.Load<TextAsset>("localization");
        localizedTexts = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(localizationFile.text);
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedTexts.ContainsKey(currentLanguage) && localizedTexts[currentLanguage].ContainsKey(key))
        {
            return localizedTexts[currentLanguage][key];
        }
        return key;  // Если ключ не найден, возвращаем сам ключ
    }

    public void SetLanguage(string language)
    {
        currentLanguage = language;
        OnChangeLanguage?.Invoke();
    }
}
