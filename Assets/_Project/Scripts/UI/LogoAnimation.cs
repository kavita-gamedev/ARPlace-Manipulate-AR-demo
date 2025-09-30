using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    public UITween logo1;

[Header("Logo Objects")]
    public Animator logoOneAnimator;    // One-time play
    public Animator logoTwoAnimator;    // Continuous loop

    [Header("Animation Clip Names")]
    public string logoOneClipName = "LogoOneAnimation";
    public string logoTwoClipName = "LogoTwoLoop";

     void Start()
    {
        // Play logo 1 animation once
        if (logoOneAnimator != null)
        {
            logoOneAnimator.Play(0, 0, 0f);
            // Make sure it stops after playing once
            logoOneAnimator.speed = 1f;
            // StartCoroutine(StopAfterOnePlay(logoOneAnimator));
        }

        // Play logo 2 animation continuously
       
    }


    public void AnimationEnd()
    {
         if (logoTwoAnimator != null)
        {
            logoTwoAnimator.gameObject.SetActive(true);
             logoOneAnimator.gameObject.SetActive(false);
            logoTwoAnimator.Play(0, 0, 0f);
            logoTwoAnimator.speed = 1f;
        }
    }

    private System.Collections.IEnumerator StopAfterOnePlay(Animator animator)
    {
        // Get the clip length
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            float clipLength = clipInfo[0].clip.length;
            yield return new WaitForSeconds(clipLength);
            animator.speed = 0f; // Stop the animation after playing once
        }
    }
    // void Awake()
    // {
    //     LTDescr cardTransition = LeanTween.move(logo1.gameObject, logo1.posTo, 1.4f).setLoopPingPong();
    // }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
}
