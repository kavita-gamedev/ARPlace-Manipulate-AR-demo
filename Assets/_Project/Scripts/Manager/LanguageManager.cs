using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;



public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }
     public AppDataPrefab AppDataPrefab;
    [Header("Add supported languages here")]
    [SerializeField] private List<LanguageData> languageDataList = new List<LanguageData>();

    private void Awake()
    {
     //PlayerPrefs.DeleteAll();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if(PlayerPrefs.HasKey(EventVariables.LanguageID) == false)
        {
            PlayerPrefs.SetInt(EventVariables.LanguageID, 1); // Default to English
        }
        SetLanguage(PlayerPrefs.GetInt(EventVariables.LanguageID)); // Default to English
    }

    /// <summary>
    /// Set language by langID from the inspector list
    /// </summary>
    public void SetLanguage(int langID)
    {  Debug.Log($"langID: {langID}");
        string localeCode = "en-US"; // default fallback
        AppDataPrefab.GetLangCode(langID, (code) => {
            localeCode = code;
        });
        
        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        if (locale != null)
        {
             Debug.Log($"Locale found: {localeCode}");
            LocalizationSettings.SelectedLocale = locale;
            PlayerPrefs.SetInt(EventVariables.LanguageID, langID); // Save selection
        }
        else
        {
            Debug.Log($"Locale not found: {localeCode}");
        }
    }


    /// <summary>
    /// Get current locale code (e.g. "en-US")
    /// </summary>
    public string GetCurrentLanguageCode()
    {
        return LocalizationSettings.SelectedLocale != null
            ? LocalizationSettings.SelectedLocale.Identifier.Code
            : "en-US";
    }
    public string GetStringFor(string entryName)
    {
        string result = "";
        result = LocalizationSettings.StringDatabase.GetLocalizedString(EventVariables.localization_Table, entryName);

        return result;
    }

    public void GetAudioFor(string entryName, System.Action<AudioClip> onLoaded)
    {
        Debug.Log($"GetAudioFor: {entryName}");
        var handle = LocalizationSettings.AssetDatabase
            .GetLocalizedAssetAsync<AudioClip>(EventVariables.Audio_Table, entryName);

        handle.Completed += h =>
        {
            if (h.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                onLoaded?.Invoke(h.Result); // gives you the AudioClip
            }
            else
            {
                Debug.LogError($"Failed to load audio for key {entryName}");
                onLoaded?.Invoke(null);
            }
        };
    }
    
    /// <summary>
    /// Load last saved language (if any)
    /// </summary>
    public void LoadSavedLanguage()
    {
        int savedID = PlayerPrefs.GetInt(EventVariables.LanguageID, 0);
        SetLanguage(savedID);
    }
}
