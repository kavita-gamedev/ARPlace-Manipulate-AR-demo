using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
public class Setting_Screen : BaseUIScreen
{
    #region SoundMusic Event
    public delegate void ChangeMusicState();
    public static event ChangeMusicState OnMusicStateChange;

    public delegate void ChangeSoundState();
    public static event ChangeSoundState OnSoundStateChange;


    [Header("Toggle Btns")]
    public Btn_Toggle btn_soundToggle;
    public Btn_Toggle btn_MusicToggle;
    #endregion

    #region Language Event
    public TMP_Dropdown dropdown;
    public TMP_Text lang_Laebel;
    public GameObject Arrow1, Arrow2;
     public GameObject template; // The dropdown template GameObject

     public GameObject MenuBG;
     public GameObject SettingBG;
    public GameObject MenuObj;
     public GameObject SettingObj;
    #endregion
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        Arrow1.SetActive(true);
        isOpen = false;
        Arrow2.SetActive(false);
        FillLanguageDropdown(ScreenManager.Instance.AppDataPrefab.LanguageDataInfo);

        btn_soundToggle.CheckState(EventVariables.soundOn);
        btn_MusicToggle.CheckState(EventVariables.musicOn);


    }


    public void SetSetting()
    {
        SettingBG.SetActive(true);
         MenuBG.SetActive(false);
         MenuObj.SetActive(false);
         SettingObj.SetActive(true);
        ScreenManager.Instance.ActivateScreen<Setting_Screen>();
    }

     public void SetMenuPopup()
    {
         SettingBG.SetActive(false);
         MenuBG.SetActive(true);
         MenuObj.SetActive(true);
         SettingObj.SetActive(false);
        ScreenManager.Instance.ActivateScreen<Setting_Screen>();
    }

    private void FillLanguageDropdown(List<AppDataPrefab.LanguageData> allIDs)
    {

        //dropdown.ClearOptions(); // remove anything already there
        if (dropdown.options.Count > 0)
        {
            SetLanguageLabel(PlayerPrefs.GetInt(EventVariables.LanguageID));
            return;
        }
        else
        {

            List<string> options = new List<string>();
            foreach (AppDataPrefab.LanguageData lang in allIDs)
            {
                options.Add(lang.language); // just the display name
            }

            dropdown.AddOptions(options);
            SetLanguageLabel(PlayerPrefs.GetInt(EventVariables.LanguageID));
            // Optional: load saved language
        }
    }

    public override void OnButtonPressed()
    {
       
        

        base.OnButtonPressed();
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        switch (btnName)
        {
            case "Btn_Sound":
                OnToggleBtn_Sound();
                break;

            case "Btn_Music":
                OnToggleBtn_Music();
                break;

            default:
                break;

        }
    }

    #region Toggle Btn
    public void OnToggleBtn_Sound()
    {
     
         if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();

            btn_soundToggle.UpdateState();
        Debug.Log(OnSoundStateChange);

        if (OnSoundStateChange != null)
            OnSoundStateChange();

    }

    public void OnLanguageClick()
    {
      if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();

            btn_soundToggle.UpdateState();
        Debug.Log(OnSoundStateChange);

        if (OnSoundStateChange != null)
            OnSoundStateChange();

    }

    public void OnToggleBtn_Music()
    {
     
            
            btn_MusicToggle.UpdateState();
        Debug.Log(OnSoundStateChange);

        if (OnMusicStateChange != null)
            OnMusicStateChange();

    }
    #endregion
    void SetLanguageLabel(int langID)
    {
        Debug.Log("Dropdown index changed to: " + langID);
        dropdown.value = langID - 1;
        // string langName = LanguageManager.Instance.AppDataPrefab.GetLangName(langID);
        // lang_Laebel.text = langName;
    }

    public void OnPrivacyClick()
    {
        Application.OpenURL(EventVariables.PrivacyPolicy);
    }

    public void OnShareGameClick()
    {
        Application.OpenURL(EventVariables.ShareGame);
    }
    public void OnDropDwon()
    {

         
        Debug.Log("Dropdown clicked" + isOpen);
        if (isOpen)
        {
            Arrow1.SetActive(true);
            Arrow2.SetActive(false);
             isOpen = false;

        }
        else
        {
            isOpen = true;
            Arrow1.SetActive(false);
            Arrow2.SetActive(true);

        }
       // isOpen = !isOpen;
        Debug.Log("after Dropdown clicked"+isOpen);
       

    }
    public void OnLanguageChanged()
    {
         isOpen = false;
         Arrow1.SetActive(true);
         Arrow2.SetActive(false);
        Debug.Log("Dropdown index changed to: " + dropdown.value);
        int langID = dropdown.value + 1; // because dropdown is zero-based
        LanguageManager.Instance.SetLanguage(langID);

       
    }

    public void OnClick_Close()
    {
        // ScreenManager.Instance.ActivateScreen<MainMenu_Screen>();
        ScreenManager.Instance.DeactivateScreen<Setting_Screen>();
    }
    public void OnClick_Home()
    {
        ScreenManager.Instance.DeactivateScreen<Setting_Screen>();

        if ((ScreenManager.Instance.GetScreen<CompanionSelecr_Screen>().isActiveAndEnabled))
         ScreenManager.Instance.DeactivateScreen<CompanionSelecr_Screen>();

         if ((ScreenManager.Instance.GetScreen<LevelSelect_Screen>().isActiveAndEnabled))
            ScreenManager.Instance.DeactivateScreen<LevelSelect_Screen>();
       
         ScreenManager.Instance.ActivateScreen<CommonUI_Screen>();
        ScreenManager.Instance.ActivateScreen<MainMenu_Screen>();

    }

     public void OnClick_Resume()
    {
    ScreenManager.Instance.DeactivateScreen<Setting_Screen>();
    }


    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }
    public override void DeviceBackButtonPressed()
    {
        base.DeviceBackButtonPressed();
        OnClick_Close();

    }
    // public RectTransform arrow;   // Assign your Arrow RectTransform (the triangle image)

    private bool isOpen = false;

  
}
