using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class UIBtn_Animate : MonoBehaviour, IPointerClickHandler
{
     [Header("Sprites")]
    public Sprite defaultSprite;   // Normal state
    public Sprite clickedSprite; 

    [Header("Animation")]
    private float clickScale = 0.98f;  // Scale on click
    private float clickDuration = 0.2f;   // Animation speed

    private Image buttonImage;
    private Button button;
    private Vector3 originalScale;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();

        originalScale = transform.localScale;

        // Set default state
        //buttonImage.sprite = defaultSprite;
        button.interactable = false; 
          
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        

            if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBtnSound();

        // Play click animation
        StopAllCoroutines();
        StartCoroutine(ClickAnim());
    }

    private IEnumerator ClickAnim()
    {
        Debug.Log("ClickAnim");
        
        if (clickedSprite != null)
            buttonImage.sprite = clickedSprite;

        // Shrink
        transform.localScale = originalScale * clickScale;
        Debug.Log("localScale "+transform.localScale);
        yield return new WaitForSeconds(clickDuration);

        // Restore
        transform.localScale = originalScale;

         if (defaultSprite != null)
        buttonImage.sprite = defaultSprite;

        button.onClick.Invoke();
        button.interactable = true;
    }

}
