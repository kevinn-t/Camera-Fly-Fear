using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera[] cams;

    int camIndex;
    public BirdPhotoCamera cameraManager;

    void Start()
    {
        if (cams == null || cams.Length == 0)
        {
            Debug.LogError("No cameras have been assigned to the SwitchCamera script.");
            return;
        }

        camIndex = 0;

        foreach (Camera cam in cams)
        {
            cam.gameObject.SetActive(false);
        }

        cams[camIndex].gameObject.SetActive(true);

        if (cameraManager != null)
        {
            cameraManager.currentActiveCamera = cams[camIndex];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            cams[camIndex].gameObject.SetActive(false);

            camIndex--;

            if (camIndex < 0)
            {
                camIndex = cams.Length - 1;
            }

            cams[camIndex].gameObject.SetActive(true);

            if (cameraManager != null)
            {
                Debug.Log("Switching camera to " + cams[camIndex].name);
                cameraManager.currentActiveCamera = cams[camIndex];
            }
        }
        if (Input.GetKeyDown("d"))
        {
            cams[camIndex].gameObject.SetActive(false);
            camIndex++;
            if (camIndex > cams.Length - 1)
            {
                camIndex = 0;
            }
            
            cams[camIndex].gameObject.SetActive(true);

            if (cameraManager != null)
            {
                Debug.Log("Switching camera to " + cams[camIndex].name);
                cameraManager.currentActiveCamera = cams[camIndex];
            }
        }
    }
}
