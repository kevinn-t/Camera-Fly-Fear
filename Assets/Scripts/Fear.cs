using UnityEngine;
using UnityEngine.UI;

public class FearMeter : MonoBehaviour
{
    [Header("Fear Settings")]
    public float fearLevel = 0f;
    public float maxTransparency = 0f;

    [Header("Audio Sources")]
    public AudioSource breathingAudio;
    public AudioSource jumpscareAudio;
    public AudioSource crowAudio;

    public AudioSource heartbeatAudio;

    [Header("State Flags")]
    public bool isJumpscareActivated = false;
    public bool isBreathingActivated = false;

    [Header("References")]
    public CanvasGroup panelCanvasGroup;

    void Update()
    {
        UpdatePanelTransparency();
        UpdateCrowVolume();
        UpdateHeartbeatVolume();

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Increasing fear level");
            increaseFear(0.1f);
        }

        if (fearLevel >= 1.0f && !isBreathingActivated)
        {
            Debug.Log("Breathing sound started");
            breathingAudio.Play();
            isBreathingActivated = true;

        }

        if (fearLevel >= 2.0f && !isJumpscareActivated && !breathingAudio.isPlaying)
        {
            Debug.Log("Jumpscare triggered!");
            jumpscareAudio.Play(0);
            isJumpscareActivated = true;
        }
    }

    public void increaseFear(float amount)
    {
        fearLevel += amount;
        Debug.Log("Fear level increased to: " + fearLevel);
    }

    void UpdatePanelTransparency()
    {
        panelCanvasGroup.alpha = fearLevel * maxTransparency;
    }

    void UpdateCrowVolume()
    {
        float targetVolume = Mathf.Clamp01(1f - (fearLevel / 2f));
        crowAudio.volume = targetVolume;

        if (!crowAudio.isPlaying && fearLevel < 1.5f)
        {
            crowAudio.Play();
        }
    }
    
    void UpdateHeartbeatVolume()
    {
        float targetVolume = Mathf.Clamp01((fearLevel - 1f) / 1f); // Same as (fearLevel - 1f)
        heartbeatAudio.volume = targetVolume;

        if (!heartbeatAudio.isPlaying && fearLevel > 1f)
        {
            heartbeatAudio.Play();
        }
    }
}