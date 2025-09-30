using System.Collections.Generic;
using UnityEngine;

public class Info_Screen : BaseUIScreen
{
    public List<InfoData> infoDataList;
    public TMPro.TMP_Text titleText;
    public TMPro.TMP_Text bodyText;

    private int currentIndex = 0;
   public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        currentIndex = 0;
        ShowCurrentInfo();
    }
    public void ShowCurrentInfo()
    {
    if (infoDataList == null || infoDataList.Count == 0) return;

    var data = infoDataList[currentIndex];
        titleText.text = LanguageManager.Instance.GetStringFor(data.title);
        bodyText.text = LanguageManager.Instance.GetStringFor(data.Body);
    }
    public void OnClick_Next()  
    {
    if (infoDataList == null || infoDataList.Count == 0) return;
    currentIndex = (currentIndex + 1) % infoDataList.Count;
    ShowCurrentInfo();
    }

    public void OnClick_Previous()
    {
        if (infoDataList == null || infoDataList.Count == 0) return;
        currentIndex = (currentIndex - 1 + infoDataList.Count) % infoDataList.Count;
        ShowCurrentInfo();
    }

    public void OnClick_Close()
    {
        // ScreenManager.Instance.ActivateScreen<MainMenu_Screen>();
        ScreenManager.Instance.DeactivateScreen<Info_Screen>();
    }

    public override void DeviceBackButtonPressed()
    {
        base.DeviceBackButtonPressed();
        OnClick_Close();

    }
    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }


}
