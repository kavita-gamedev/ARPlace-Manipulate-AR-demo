using UnityEngine;
using System.Collections;
// using DG.Tweening;

public class BaseUIScreen : MonoBehaviour, IScreen
{
    //public float timeduration
    /// <summary>
    /// Activate the current screen gameobject.
    /// </summary>
    public void Activate()
    {
       
        gameObject.SetActive(true);

        //m_dgEntryAnimationFunction = EntryAnimationOver;
        //m_dgExitAnimationFunction = ExitAnimationOver;
        OnScreenEnabled();
    }

    /// <summary>
    /// Deactivate the Current screen gameobject.
    /// </summary>
    public void Deactivate()
    {
        gameObject.SetActive(false);
        OnScreenDisabled();
    }


    /// <summary>
    /// Raises the back button pressed event for this screen.
    /// </summary>
    public virtual void OnBackButtonPressed()
    {
        //Debug.Log("Back");
    }

    /// <summary>
    /// Raises the screen enabled event.
    /// </summary>
    public virtual void OnScreenEnabled()
    {
        //m_Entryanimation();
    }

    //public void m_Entryanimation()
    //{
    //    //UIEntryAnimtion();
    //}

    //public virtual void UIEntryAnimtion()
    //{
    //    //if(ScreenManager.Instance.GetScreen<MainmenuScreen>().isActiveAndEnabled || ScreenManager.Instance.GetScreen<KathikaScreen>().isActiveAndEnabled )//ScreenManager.Instance.GetScreen<CommonUIScreen>().isActiveAndEnabled)
    //    //{

    //    //}
    //    //else
    //    {
    //        gameObject.SetActive(true);
    //        Sequence entryTween = DOTween.Sequence();
    //        entryTween.Append(gameObject.transform.DOScale(1.1f, 0.15f).From(0f, false));
    //        entryTween.Append(gameObject.transform.DOScale(1f, 0.1f));
    //        entryTween.OnComplete(() =>
    //        {
    //            m_dgEntryAnimationFunction();
    //            //entrycompleted();
    //        });

    //    }

    //}

    //public virtual void EntryAnimationOver()
    //{
    //    Debug.Log("EntryAnimationOver______");
    //    //Debug.Log(m_PageId.ToString());
    //    //OnStart();
    //    //m_PageAnimator.enabled = false;
    //}

    //public virtual void entrycompleted()
    //{
    //    Debug.Log("entrycompleted_______________");
    //}

    private delegate void AnimationOver();
    private AnimationOver m_dgEntryAnimationFunction;
    private AnimationOver m_dgExitAnimationFunction;
    /// <summary>
    /// Raises the screen disabled event.
    /// </summary>
    public virtual void OnScreenDisabled()
    {

    }

    /// <summary>
    /// Raises the button pressed event.
    /// </summary>
    public virtual void OnButtonPressed()
    {

    }

    /// <summary>
    /// Handle the back button pressed event for a screen.
    /// </summary>
    public virtual void DeviceBackButtonPressed()
    {
        
    }

    void Update()
    {
        // Exit Sample  

        if (IsEscapePressed())
        {
            DeviceBackButtonPressed();
        }
    }

    bool IsEscapePressed()
    {
        //#if ENABLE_INPUT_SYSTEM
        //            return Keyboard.current != null ? Keyboard.current.escapeKey.isPressed : false; 
        //#else
        return Input.GetKeyDown(KeyCode.Escape);
        //#endif
    }

    /// <summary>
    /// Determines whether this screen is enabled.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <value><c>true</c> if this instance is screen enabled; otherwise, <c>false</c>.</value>
    public bool IsScreenEnabled
    {
        get{ return gameObject.activeSelf; }
    }
}
