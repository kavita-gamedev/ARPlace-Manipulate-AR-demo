using UnityEngine;
using UnityEngine.SceneManagement;
public class Disclaimer_Screen : BaseUIScreen
{
    
    public GameObject logo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        logo.SetActive(false);

    }

   public void OnNextClick()
    {
      

        if (PlayerPrefs.GetInt("TutorialShown") == 0)
        {
            ScreenManager.Instance.ActivateScreen<Tutorial_Screen>();
            ScreenManager.Instance.DeactivateScreen<Disclaimer_Screen>();
            return;
        }
        else
        {
            LoadScene();
        }
       
   }
    void LoadScene()
    {
        SceneManager.LoadScene((int)Game.Scenes.Mainmenu);
    }
    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        ScreenManager.Instance.RemoveScreen(this);
    }
}
