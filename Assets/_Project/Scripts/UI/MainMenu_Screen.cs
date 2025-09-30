using System;
using UnityEngine;

public class MainMenu_Screen : BaseUIScreen
{
    public GameObject logo;
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        ScreenManager.Instance.GetScreen<CommonUI_Screen>().lastActivatedScreen = this;

        logo.SetActive(true);
    }

   
    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        logo.SetActive(false);
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }

    public override void DeviceBackButtonPressed()
    {
        base.DeviceBackButtonPressed();

        if ((ScreenManager.Instance.GetScreen<Info_Screen>().isActiveAndEnabled)||
         (ScreenManager.Instance.GetScreen<Setting_Screen>().isActiveAndEnabled))
            return;
            
        ScreenManager.Instance.ActivateScreen<ExitScreen>();
    }
    

    public void OnClick_Play()
    {


        if (PlayerPrefs.GetInt(EventVariables.CameraPermissionGranted) == 1)
        {
            ScreenManager.Instance.ActivateScreen<LevelSelect_Screen>();
            ScreenManager.Instance.DeactivateScreen<MainMenu_Screen>();
            return;
        }
        else
        {
            ScreenManager.Instance.ActivateScreen<Permission_Screen>();
            ScreenManager.Instance.DeactivateScreen<MainMenu_Screen>();
        }
    }

}
