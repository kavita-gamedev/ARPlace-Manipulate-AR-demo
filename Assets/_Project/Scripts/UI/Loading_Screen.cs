using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class Loading_Screen : BaseUIScreen
{
    public Slider sld_progressBar;
    public GameObject reporter;
    //  public UITween logo1;  
    [SerializeField] TMP_Text txt_Progress;
    private bool isLoadingScene = false;
    public void Awake()
    {
        ScreenManager.Instance.DeactivateScreen<LanguageSelect_Screen>();
    }
    public override void OnScreenEnabled()
    {
        base.OnScreenEnabled();
       
        isLoadingScene = false;
        // LTDescr cardTransition = LeanTween.move(logo1.gameObject, logo1.posTo, 1.4f).setLoopPingPong();
    }

     void OnEnable()
    {
        Messenger<float>.AddListener(EventVariables.userLoginSuccess, OnUserFetchedSuccess);
      
    }

    void OnDisable()
    {
        Messenger<float>.RemoveListener(EventVariables.userLoginSuccess, OnUserFetchedSuccess);
       
    }
    void Start()
    {
        StartCoroutine(FakeProgressRoutine());
    }
    // IEnumerator Start()
    // {
    //     yield  return null;
       
    //     Messenger<float>.Broadcast(EventVariables.userLoginSuccess, 0.80f, MessengerMode.DONT_REQUIRE_LISTENER);
    // }

  private IEnumerator FakeProgressRoutine()
{
    // Fake steps: 20%, 50%, 80%, 100%
    float[] progressSteps = { 0.2f, 0.5f, 1f };

    foreach (float step in progressSteps)
    {
        // Smoothly move to next step
        float startValue = sld_progressBar.value;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime; // speed of animation
            float newValue = Mathf.Lerp(startValue, step, t);
            Messenger<float>.Broadcast(EventVariables.userLoginSuccess, newValue, MessengerMode.DONT_REQUIRE_LISTENER);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // pause between stages
    }
}
    void OnUserFetchedSuccess(float mValue)
    {
        Debug.Log("mValue: " + mValue);
        sld_progressBar.value = mValue;

        if (mValue > 0.75 && !isLoadingScene)
        {
            isLoadingScene = true;

            ScreenManager.Instance.ActivateScreen<LanguageSelect_Screen>();
            ScreenManager.Instance.DeactivateScreen<Loading_Screen>();
            //StartCoroutine(LoadScene());
        }
        else
        {

        }
    }

    private IEnumerator LoadScene()
    {
        Debug.Log("LoadScene: ");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)Game.Scenes.Mainmenu);

        asyncLoad.allowSceneActivation = false;
        float startValue = 0.8f;

        while (asyncLoad.progress < 0.9f)
        {
            startValue += asyncLoad.progress * 0.5f;
            //Add up the current level load progress with older progress.
            sld_progressBar.value = startValue + (asyncLoad.progress * 0.45f);
            //progressBar.value = (asyncLoad.progress);
            yield return null;
        }

        float leftOverValue = sld_progressBar.value;
        while (leftOverValue < 1f)
        {
            leftOverValue += 0.01f;
            sld_progressBar.value = leftOverValue;
            yield return null;
        }
        yield return null;
        sld_progressBar.value = 1;
        yield return null;
        asyncLoad.allowSceneActivation = true;
        
     }

      public void OnProgressValueChange()
    {

        float progress = (sld_progressBar.value / sld_progressBar.maxValue) * 100f;
        //Debug.Log("OnProgressValueChange" + progress);
        txt_Progress.text = progress.ToString("0") + "%";

        // Pradeep Work
        if (progress < 10)
            txt_Progress.text = (progress).ToString("0") + "%";
        else if (progress < 100)
            txt_Progress.text = (progress).ToString("00") + "%";
        else
            txt_Progress.text = (progress).ToString("000") + "%";

        sld_progressBar.value = progress * 0.01f;

    }

    public override void OnScreenDisabled()
    {
        base.OnScreenDisabled();
        this.gameObject.SetActive(false);
        
         ScreenManager.Instance.RemoveScreen(this);
    }

    public void setDebuggingmode(bool debug)
    {
#if UNITY_EDITOR
        if (debug)
        {
            if (reporter != null)
            {
                reporter.SetActive(true);
            }
            Debug.unityLogger.logEnabled = true;
        }
        else
        {
            if (reporter != null)
            {
                reporter.SetActive(true);
            }
            Debug.unityLogger.logEnabled = true;
        }
#else
        if (debug)
        {
            if (reporter != null)
            {
                reporter.SetActive(true);
            }
            Debug.unityLogger.logEnabled = true;
        }
        else
        {
            if (reporter != null)
            {
                reporter.SetActive(false);
            }
            Debug.unityLogger.logEnabled = false;
        }

#endif
        //Debug.unityLogger.logEnabled = true;
    }
}
