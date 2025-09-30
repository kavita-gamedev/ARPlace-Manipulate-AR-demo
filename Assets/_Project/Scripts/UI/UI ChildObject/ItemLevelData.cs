using UnityEngine;
using TMPro;
// using UnityEngine.SceneManagement;

public class ItemLevelData : MonoBehaviour
{
    public int ID;
     public string s_name;
      public TMP_Text lbl_name;
       public TMP_Text lbl_subMsg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void setdata()
    {

    }

    public void OnClick()
    {
            if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();

        ScreenManager.Instance.GetScreen<CompanionSelecr_Screen>().SetCompanionData(ID);  
        ScreenManager.Instance.DeactivateScreen<LevelSelect_Screen>();

    }
    
}
