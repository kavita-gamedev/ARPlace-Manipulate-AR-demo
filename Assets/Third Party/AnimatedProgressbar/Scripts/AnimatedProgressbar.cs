using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class AnimatedProgressbar : MonoBehaviour
{
    [SerializeField] private Image m_barUI = null;
    [SerializeField] private float m_speed = 0f;

    [SerializeField] private TextMeshProUGUI m_progressText;
    [SerializeField] private float m_fillDuration = 3f;

    private Material m_runtimeMaterial;
    private float m_fillAmount = 0f;
    private float m_timer = 0f;
    private bool m_isFilling = false;

    public UnityEvent onFinished;

    public float FillAmount
    {
        get => m_fillAmount;
        set
        {
            m_fillAmount = Mathf.Clamp01(value);

            if (m_barUI != null)
            {
                m_barUI.fillAmount = m_fillAmount;
            }

            UpdateProgressText();
        }
    }

    private void Awake()
    {
        if (m_barUI == null)
        {
            Debug.LogError("AnimatedProgressbar: Image reference not assigned.");
            return;
        }

        m_barUI.type = Image.Type.Filled;
        m_barUI.fillMethod = Image.FillMethod.Horizontal;

        if (m_barUI.material != null)
        {
            m_runtimeMaterial = new Material(m_barUI.material);
            m_barUI.material = m_runtimeMaterial;
        }

        FillAmount = m_fillAmount;
    }

    private void Start()
    {
        StartFilling();
    }

    private void Update()
    {
        if (m_runtimeMaterial != null)
        {
            Vector2 offset = m_runtimeMaterial.mainTextureOffset;
            offset.x -= m_speed * Time.deltaTime;
            m_runtimeMaterial.mainTextureOffset = offset;
        }

        if (m_isFilling)
        {
            m_timer += Time.deltaTime;
            float t = Mathf.Clamp01(m_timer / m_fillDuration);
            FillAmount = Mathf.Lerp(0f, 1f, t);
           
            if (t >= 1f)
            {
                m_isFilling = false;
                onFinished?.Invoke();
            }
        }
    }

    private void UpdateProgressText()
    {
        if (m_progressText != null)
        {
            m_progressText.text = $"{(m_fillAmount * 100f):F0}%";
        }
    }

    public void StartFilling()
    {
        m_timer = 0f;
        m_isFilling = true;
        FillAmount = 0f;
    }
}