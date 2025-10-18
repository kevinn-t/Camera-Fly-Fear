using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    Camera[] cams;
    int camIndex;
    InputAction interactButton;

    void Start()
    {
        camIndex = 0;
        cams = Camera.allCameras;
        foreach (Camera cam in cams)
            cam.gameObject.SetActive(false);

        cams[camIndex].gameObject.SetActive(true);

        interactButton = InputSystem.actions.FindAction("Interact");
        Debug.Log("balls");
    }

    void Update()
    {
        if (interactButton.IsPressed())
        {
            Debug.Log("pressed interact");
            camIndex++;

            if (camIndex < cams.Length)
            {
                cams[camIndex-1].gameObject.SetActive(false);
                cams[camIndex].gameObject.SetActive(true);
            }
            else
            {
                cams[camIndex - 1].gameObject.SetActive(false);
                camIndex = 0;
                cams[camIndex].gameObject.SetActive(true);
            }
        }

    }
}
