using UnityEngine;
using UnityEngine.UI;

public class FearMeter : MonoBehaviour
{
    [Header("Fear Settings")]
    public float fearLevel = 0f;
    public float maxTransparency = 0f;
    public float amount = 0.1f;

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
        //Testing code to increase fear level
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Debug.Log("Increasing fear level");
        //    increaseFear();
        //}

        if (fearLevel >= 1.0f)
        {
            Time.timeScale = 0f; // Freeze the game
        }

    }

    public void increaseFear()
    {
        fearLevel += amount;
        Debug.Log("Fear level increased to: " + fearLevel);
    }

    void UpdatePanelTransparency()
    {
        panelCanvasGroup.alpha = fearLevel * maxTransparency;
    }
}