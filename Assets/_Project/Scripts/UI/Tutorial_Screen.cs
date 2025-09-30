using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
public class Tutorial_Screen : BaseUIScreen
{
    public TMP_Text tutorialText;       // Assign your UI text component
    public UITween RobotAnimation;  
    private int currentStep = 0;
    public float typingDuration = 4f; // Duration for typing effect
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
        // LTDescr cardTransition = LeanTween.move(RobotAnimation.gameObject, RobotAnimation.posTo, 1.4f).setLoopPingPong();
        PlayerPrefs.SetInt("TutorialShown", 0);
        currentStep = 0;
        ShowStep();
    }

    void ShowStep()
    {
        int tutorialSteps = ScreenManager.Instance.AppDataPrefab.TutorialDataInfo.Count;
        Debug.Log("Tutorial Steps: " + tutorialSteps + " Current Step: " + currentStep);
        if (currentStep < tutorialSteps)
        {

            string _title = ScreenManager.Instance.AppDataPrefab.TutorialDataInfo[currentStep].Title;
            string __body = ScreenManager.Instance.AppDataPrefab.TutorialDataInfo[currentStep].Body;
            Debug.Log("_title Steps: " + _title);
            PlayTypingEffect(LanguageManager.Instance.GetStringFor(_title));
            LanguageManager.Instance.GetAudioFor(_title, clip =>
            {
                if (clip != null)
                {
                    AudioManager.Instance.PlayTitorialSound(clip);
                   
                }
            });
        }
        else
        {
            EndTutorial();
        }
    }

     public void OnNextClicked()
    {
     

        currentStep++;
        ShowStep();
    }

   public void OnSkipClicked()
    {
     

        EndTutorial();
    }

     void EndTutorial()
    {
        PlayerPrefs.SetInt("TutorialShown", 1); // Save that tutorial was shown
        PlayerPrefs.Save();

        LoadScene();
    }
     void LoadScene()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic(AudioManager.Instance.Tutorial_AudioSource);
        }
        SceneManager.LoadScene((int)Game.Scenes.Mainmenu);
    }



    public void PlayTypingEffect(string newText)
    {
         int totalChars = newText.Length;
        LeanTween.value(gameObject, 0, totalChars, typingDuration)
        .setOnUpdate((float val) =>
        {
            int charCount = Mathf.FloorToInt(val);
            tutorialText.text = newText.Substring(0, charCount);
        });
    }

    

    // Update is called once per frame
    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        ScreenManager.Instance.RemoveScreen(this);
    }
}
