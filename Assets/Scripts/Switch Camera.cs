using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    Camera[] cams;
    int camIndex;

    void Start()
    {
        camIndex = 0;
        cams = Camera.allCameras;
        foreach (Camera cam in cams)
        {
            cam.gameObject.SetActive(false);
            Debug.Log(cam.gameObject.name);
        }

        cams[camIndex].gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            camIndex--;

            if (camIndex >= 0)
            {
                cams[camIndex].gameObject.SetActive(true);
                cams[camIndex + 1].gameObject.SetActive(false);
            }
            else
            {
                cams[camIndex + 1].gameObject.SetActive(false);
                camIndex = cams.Length - 1;
                cams[camIndex].gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown("d"))
        {
            camIndex++;

            if (camIndex <= cams.Length - 1)
            {
                cams[camIndex].gameObject.SetActive(true);
                cams[camIndex - 1].gameObject.SetActive(false);
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
