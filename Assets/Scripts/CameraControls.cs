using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControls : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 30.0f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 25.0f;
    public float minZoomFOV;
    public float maxZoomFOV;

    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(-rotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            Debug.Log("rotating");
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            Debug.Log("rotating");
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
            Debug.Log("rotating");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
            Debug.Log("rotating");
        }

        float currentFOV = cam.fieldOfView;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentFOV > minZoomFOV)
            {
                currentFOV -= zoomSpeed * Time.deltaTime;
                Debug.Log("zooming");
            }

        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (currentFOV < maxZoomFOV)
            {
                currentFOV += zoomSpeed * Time.deltaTime;
                Debug.Log("zooming");
            }
        }

        cam.fieldOfView = currentFOV;
    }
}
