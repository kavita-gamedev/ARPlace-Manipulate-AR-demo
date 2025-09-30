using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Splash_Screen : MonoBehaviour
{

    public UITween logo;
    public UITween logo1;  
    public UITween logo2;
    void Start()
    {
        // PlayAnimationLogo1();
        // PlayAnimationLogo2();
         Invoke("LoadScene", 0.5f);

    }
    void PlayAnimationLogo1()
    {
        LTDescr trasnsition = LeanTween.scale(logo1.gameObject, logo1.scaleTo, logo1.tweenDuration).setEaseOutBounce();
        trasnsition.setOnComplete(() =>
        {
            // PlayAnimationLogo2();
           
        });
    }

    void PlayAnimationLogo2()
    {
        LTDescr trasnsition = LeanTween.scale(logo2.gameObject, logo2.scaleTo, logo2.tweenDuration).setEaseOutBounce();
        trasnsition.setOnComplete(() =>
        {
            Invoke("LoadScene", 0.1f);
            //GameController.Instance.LoadScene((int) ̰Scenes.Loading);
        });
    }

    void LoadScene()
    {
        SceneManager.LoadScene((int)Game.Scenes.Loading);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
}