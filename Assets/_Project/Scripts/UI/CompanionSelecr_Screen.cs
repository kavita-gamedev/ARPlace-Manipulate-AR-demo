using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using UnityEngine.SceneManagement;
public class CompanionSelecr_Screen : BaseUIScreen
{

    public TMP_Text txt_Score;
    public List<ItemCompanion> itemCompoanionDatas;
    private List<ItemCompanion> spawnedCompanions = new List<ItemCompanion>();
    [SerializeField] private Transform companionParent;   // Parent UI container (Grid/VerticalLayout)
    [SerializeField] private GameObject companionPrefab; // Prefab for each companion UI item
                                                         // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TMP_Text txt_levelTitle;
    private int lastlevelID = -1;
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        // SetCompanionData();

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

    public void SetCompanionData(int LevelID)
    {
        ScreenManager.Instance.ActivateScreen<CompanionSelecr_Screen>();
        Debug.Log("Set Companion Data for Level ID: " + LevelID);

        var levels = ScreenManager.Instance.AppDataPrefab.LevelDataInfo;
        var level = levels.Find(l => l.id == LevelID);
        txt_levelTitle.text = LanguageManager.Instance.GetStringFor(level.title);
        if (level == null) return;

        if (lastlevelID == LevelID)
            return;

        int totalCompanions = level.CompanionDataInfo.Count;

        // Loop through level companions
        for (int i = 0; i < totalCompanions; i++)
        {
            if (i < spawnedCompanions.Count)
            {
                // Update existing item
                spawnedCompanions[i].screen = this;
                spawnedCompanions[i].SetItem(level.CompanionDataInfo[i].id, level.CompanionDataInfo[i].img, level.CompanionDataInfo[i].name);
            }
            else
            {
                // Create new one if not enough
                GameObject go = Instantiate(companionPrefab, companionParent);
                var itemUI = go.GetComponent<ItemCompanion>();
                itemUI.screen = this;
                itemUI.SetItem(level.CompanionDataInfo[i].id, level.CompanionDataInfo[i].img, level.CompanionDataInfo[i].name);
                spawnedCompanions.Add(itemUI);
            }
        }

        // OPTIONAL: If spawned list has more than needed, you can hide them
        for (int i = totalCompanions; i < spawnedCompanions.Count; i++)
        {
            spawnedCompanions[i].gameObject.SetActive(false);
        }

        lastlevelID = LevelID;
      //  txt_levelTitle.text = level.title;
        txt_levelTitle.text = LanguageManager.Instance.GetStringFor(level.title);
    }

    public void OnClick_Character(int companionID)
    {
        foreach (var item in spawnedCompanions)
        {
            if (item.ID == companionID)
            {
                item.BGicon.color = Color.yellow;
            }
            else
            {
                item.BGicon.color = Color.white;
            }
        }
        // lOadGamePlay();
        SetUltilitySelection();

    }
    public void OnClick_Menu()
    {
         ScreenManager.Instance.GetScreen<Setting_Screen>().SetMenuPopup();
        //   ScreenManager.Instance.ActivateScreen<Setting_Screen>();
        // Clear previously spawned companions

    }

    void OnBack()
    {
    if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();
            
        ScreenManager.Instance.DeactivateScreen<CompanionSelecr_Screen>();
        ScreenManager.Instance.ActivateScreen<LevelSelect_Screen>();
    }

    void SetUltilitySelection()
    {
        ScreenManager.Instance.DeactivateScreen<CompanionSelecr_Screen>();
        ScreenManager.Instance.ActivateScreen<UtilitySelection_Screen>();
    }
    // public void lOadGamePlay()
    // {
    //     SceneManager.LoadScene((int)Game.Scenes.GamePlay);
    // }

    public override void DeviceBackButtonPressed()
    {
        base.DeviceBackButtonPressed();

        if(ScreenManager.Instance.GetScreen<Setting_Screen>().isActiveAndEnabled)
            return;
            
        OnBack();

    }

    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }
}
  

