using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    public static FearMeter fearLevel;
    public AudioSource jumpscareAudio; 
    void Update()
    {
        if (fearLevel.fearLevel >= 2.0)
        {
            Debug.Log("Jumpscare triggered!");
            jumpscareAudio.Play();
        }
    }
}
