using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class LanguageSelect_Screen : BaseUIScreen
{
    public List<Toggle> ListToggle_Language;
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        InstanytiateLanguageToggles();
        // SelastLastSelectedLanguage();
        //LanguageManager.Instance.SetLanguage(1);    // Default to English

    }
    void InstanytiateLanguageToggles()
    {
        var languages = ScreenManager.Instance.AppDataPrefab.LanguageDataInfo;
        int totalLanguages = languages.Count;
        if (totalLanguages > 0)
        {
            SelectLastSelectedLanguage();
            return;
        }
        else
        {

            for (int i = 0; i < totalLanguages; i++)
            {
                if (i < ListToggle_Language.Count)
                {
                    // Update existing item
                    ListToggle_Language[i].GetComponentInChildren<TMPro.TMP_Text>().text = languages[i].language;
                    int langID = languages[i].id; // Capture the current language ID
                    ListToggle_Language[i].onValueChanged.AddListener((isOn) =>
                    {
                        if (isOn)
                        {
                            OnLangClick(langID);
                        }
                    });
                }
                else
                {
                    // Create new one if not enough
                    Debug.LogWarning("Not enough toggle items in the list to display all languages.");
                }
            }
             SelectLastSelectedLanguage();
        }

       
    }
    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }

   void SelectLastSelectedLanguage()
    {
        // Load saved language ID (default 1 = English if not set)
        int savedLangID = PlayerPrefs.GetInt(EventVariables.LanguageID, 1);
        Debug.Log($"Restoring Language ID: {savedLangID}");

        var languages = ScreenManager.Instance.AppDataPrefab.LanguageDataInfo;

        for (int i = 0; i < languages.Count && i < ListToggle_Language.Count; i++)
        {
            int langID = languages[i].id;

            // This will turn on the correct toggle and off others automatically (if in a ToggleGroup)
            ListToggle_Language[i].isOn = (langID == savedLangID);
        }
    }

    public void OnLangClick(int langID)
    {
            if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();

        LanguageManager.Instance.SetLanguage(langID);
    }

    public void OnNextlick()
    {
    
        Debug.Log("Next Clicked - Proceeding to Disclaimer Screen");
        ScreenManager.Instance.ActivateScreen<Disclaimer_Screen>();
         ScreenManager.Instance.DeactivateScreen<LanguageSelect_Screen>();
    }
}
