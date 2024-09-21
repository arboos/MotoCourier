using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using YG;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    private Dictionary<string, Dictionary<string, string>> localizedTexts;
    public string currentLanguage = "ru";  // Язык по умолчанию
    public static Action OnChangeLanguage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadLocalization();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //currentLanguage = !string.IsNullOrEmpty(YandexGame.savesData.language) ? YandexGame.savesData.language : "ru";
        currentLanguage = PlayerPrefs.HasKey("language") ? PlayerPrefs.GetString("language") : "ru"; 

        SetLanguage(currentLanguage);
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
        
        //YandexGame.savesData.language = currentLanguage;  
        //YandexGame.SaveProgress();  
        
        PlayerPrefs.SetString("language", currentLanguage);

        OnChangeLanguage?.Invoke();
    }
}

