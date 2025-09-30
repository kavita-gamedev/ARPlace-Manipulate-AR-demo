using UnityEngine;

public class ExitScreen : BaseUIScreen
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
      
    }

    public void OnClickYes()
    {
        Application.Quit();
    }

    public void OnClickNo()
    {
        ScreenManager.Instance.DeactivateScreen<ExitScreen>();
    }
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }
    public override void DeviceBackButtonPressed()
    {
        base.DeviceBackButtonPressed();
        OnClickNo();

    }
}
