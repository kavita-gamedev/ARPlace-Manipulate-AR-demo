using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LevelSelect_Screen : BaseUIScreen
{
    public List<ItemLevelData> itemLevelDatas;
     public TMP_Text txt_Score;

    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        SetLevelData();


        ScreenManager.Instance.DeactivateScreen<CommonUI_Screen>();
    }

      void OnEnable()
    {
        Messenger<int>.AddListener(EventVariables.refreshPlayerScore, RefreshPlayerScore);
    }
    void RefreshPlayerScore(int a)
    {
        txt_Score.text = a.ToString();
    }
    void OnDisable()
    {
        Messenger<int>.RemoveListener(EventVariables.refreshPlayerScore, RefreshPlayerScore);
    }

   public void OnClick_Menu()
    {
         ScreenManager.Instance.GetScreen<Setting_Screen>().SetMenuPopup();
       // ScreenManager.Instance.ActivateScreen<Setting_Screen>();
    }
    
    public void OnBackClick()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();

        ScreenManager.Instance.ActivateScreen<MainMenu_Screen>();
        ScreenManager.Instance.ActivateScreen<CommonUI_Screen>();
        ScreenManager.Instance.DeactivateScreen<LevelSelect_Screen>();
    }

     public override void DeviceBackButtonPressed()
    {
        base.DeviceBackButtonPressed();

        Debug.Log("IsEnabled"+ScreenManager.Instance.GetScreen<Setting_Screen>().isActiveAndEnabled);
        if (ScreenManager.Instance.GetScreen<Setting_Screen>().isActiveAndEnabled)
            return;
            
        OnBackClick();

    }
    
    public void SetLevelData()
    {
        int totalLevel = ScreenManager.Instance.AppDataPrefab.LevelDataInfo.Count;
        for (int i = 0; i < totalLevel; i++)
        {

            AppDataPrefab.LevelData level = ScreenManager.Instance.AppDataPrefab.LevelDataInfo[i];
            Debug.Log("Setting Level Data for index: " + i + "Level id" + level.id);
            itemLevelDatas[i].ID = level.id;
            // itemLevelDatas[i].s_name = level.title;
            // itemLevelDatas[i].lbl_name.text = level.Body;
        }
    }
    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }

}
