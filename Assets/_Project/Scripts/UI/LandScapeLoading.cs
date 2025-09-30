using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LandScapeLoading : BaseUIScreen
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        OnRotateAndLoadScene();
    }

    public void OnRotateAndLoadScene()
    {
         Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        // Force landscape
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // Optionally wait a frame to let rotation happen
        StartCoroutine(LoadGamePlay());
    }

  
    private IEnumerator LoadGamePlay()
    {
         yield return null;
        SceneManager.LoadScene((int)Game.Scenes.GamePlay);
    }


    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }
}
