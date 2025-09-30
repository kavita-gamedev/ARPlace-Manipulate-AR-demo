using UnityEngine;
using UnityEngine.Android;
using System;
using System.Collections;
public class Permission_Screen : BaseUIScreen
{
    private string cameraPermission = "android.permission.CAMERA";
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();

    }

    public void OnClick_Next()
    {
         StartCoroutine(RequestCameraPermission(value =>
                {
                    if (value) {
                        PlayerPrefs.SetInt(EventVariables.CameraPermissionGranted, 1);
                          ScreenManager.Instance.ActivateScreen<LevelSelect_Screen>();
                        ScreenManager.Instance.DeactivateScreen<Permission_Screen>();
                    }
                }));
      
    }
    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }
    

     private IEnumerator RequestCameraPermission(Action<bool> value)
    {
        if (!Permission.HasUserAuthorizedPermission(cameraPermission))
        {
            Permission.RequestUserPermission(cameraPermission);

            while (!Permission.HasUserAuthorizedPermission(cameraPermission))
            {
                yield return null;
            }
        }

        if (Permission.HasUserAuthorizedPermission(cameraPermission))
        {
            value?.Invoke(true);
        }
        else
        {
            value?.Invoke(false);
        }
    }
}
