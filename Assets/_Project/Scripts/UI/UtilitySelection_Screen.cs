using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class UtilitySelection_Screen : BaseUIScreen
{
    public List<ItemUtilityData> itemUtilityData;
    public ItemUtilityData obj_ItemUtility;
    [SerializeField] private Transform UtilityParent; 
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        SetUtilityData();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetUtilityData()
    {
        // Get and randomize UtilityData
        var utilityList = ScreenManager.Instance.GameDataPrefab.UtilityDataInfo
            .OrderBy(x => UnityEngine.Random.value) // shuffle
            .ToList();

        int totalCompanions = utilityList.Count;

        for (int i = 0; i < totalCompanions; i++)
        {
            GamePrefabs.UtilityData Utility = utilityList[i];

            if (i < itemUtilityData.Count)
            {
                // Already instantiated → just update data
                itemUtilityData[i].SetData(Utility.id, Utility.img);
                Debug.Log("Updating Utility Data for index: " + i + " | Utility id: " + Utility.id);
            }
            else
            {
                // Instantiate new if not enough items
                ItemUtilityData go = Instantiate(obj_ItemUtility, UtilityParent);
                var itemUI = go.GetComponent<ItemUtilityData>();

                itemUI.SetData(Utility.id, Utility.img);
                itemUtilityData.Add(itemUI);

                Debug.Log("Instantiating Utility Data for index: " + i + " | Level id: " + Utility.id);
            }
        }
    }

    void OnBack()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();
            
        ScreenManager.Instance.DeactivateScreen<UtilitySelection_Screen>();
        ScreenManager.Instance.ActivateScreen<CompanionSelecr_Screen>();
    }

    public override void DeviceBackButtonPressed()
    {
        base.DeviceBackButtonPressed();

        if(ScreenManager.Instance.GetScreen<Setting_Screen>().isActiveAndEnabled)
            return;
            
        OnBack();

    }

    public void OnClick_Menu()
    {
         ScreenManager.Instance.GetScreen<Setting_Screen>().SetMenuPopup();
    }
    
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }
  
}
