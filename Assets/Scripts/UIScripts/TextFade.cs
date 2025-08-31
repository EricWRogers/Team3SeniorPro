using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float waitTime;
    public float fadeRate;
    private float m_fullTime;
    private float m_currentWait;

    void Awake()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Fade();
    }
    public void ResetFade()
    {
        text.color = Color.white;
        m_currentWait = waitTime;

    }

    public void Fade()
    {
        if (m_currentWait > 0)
        {
            m_currentWait -= Time.deltaTime;
        }
        else
        {
            text.color = Color.clear;
        }
    }
}
