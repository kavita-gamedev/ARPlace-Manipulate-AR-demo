using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUtilityData : MonoBehaviour
{
    public int ID;
    public string s_name;
    public Image img_Utility;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetData(int _ID, Sprite _img)
    {
        img_Utility.sprite = _img;
        ID = _ID;

    }


    public void OnClick()
    {
        ScreenManager.Instance.GameDataPrefab.SelectedUtility = 1;
        ScreenManager.Instance.DeactivateScreen<UtilitySelection_Screen>();
        ScreenManager.Instance.ActivateScreen<LandScapeLoading>();
       
    }
    
}
