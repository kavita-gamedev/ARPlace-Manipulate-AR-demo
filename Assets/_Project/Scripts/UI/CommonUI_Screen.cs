using UnityEngine;
using TMPro;
public class CommonUI_Screen : BaseUIScreen
{
    public TMP_Text txt_Score;
    public BaseUIScreen lastActivatedScreen;

    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
       
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
    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();

    }


    public void OnClick_Setting()
    {

        ScreenManager.Instance.GetScreen<Setting_Screen>().SetSetting();
    }

    public void OnClick_Info()
    {
        Debug.Log("OnClick_Info");
        ScreenManager.Instance.ActivateScreen<Info_Screen>();
        
    }

    public void OnClick_Feedback()
    {
        Application.OpenURL(EventVariables.Feedback);
        // ScreenManager.Instance.ActivateScreen<fee>();
        // lastActivatedScreen = ScreenManager.Instance.GetScreen<Setting_Screen>();
    }
}
