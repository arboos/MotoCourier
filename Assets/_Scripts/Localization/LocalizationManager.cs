using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using YG;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    private Dictionary<string, Dictionary<string, string>> localizedTexts;
    public string currentLanguage = "ru";  // Язык по умолчанию
    public static Action OnChangeLanguage;

    [Header("Button")] 
    public Color buttonOn;
    public Color buttonOff;

    public Image[] buttons;

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
        SetLanguage(YandexGame.savesData.lang);
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
        return key;
    }

    public void SetLanguage(string language)
    {
        currentLanguage = language;

        YandexGame.savesData.lang = currentLanguage;
        YandexGame.SaveProgress();


        foreach (var button in buttons)
        {
            button.color = button.name == currentLanguage ? buttonOn : buttonOff;
        }

        OnChangeLanguage?.Invoke();
    }
}

