using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemCompanion : MonoBehaviour
{
    public Image icon;
     public Image BGicon;
    public TMP_Text txt_Name;
    public int ID;
    public CompanionSelecr_Screen screen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetItem(int _ID, Sprite _icon, string _name)
    {
        Debug.Log("Setting Item: " + _ID);
        ID = _ID;
        icon.sprite = _icon;
        txt_Name.text = _name;
         BGicon.color = Color.white;
    }

    public void OnClick_Item()
    {
          if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();
            
        Debug.Log("Clicked Item: " + ID);
        BGicon.color = Color.yellow;
        ScreenManager.Instance.AppDataPrefab.selectedCompanion = ID;
        screen.OnClick_Character(ID);
    }
}
