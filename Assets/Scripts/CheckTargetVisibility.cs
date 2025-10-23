using UnityEngine;

public class CheckTargetVisibility : MonoBehaviour
{
    public bool CheckVisibility(Transform targetObject, Camera camera)
    {
        if (targetObject == null)
        {
            return false;
        }

        if (IsInView(targetObject.position, camera))
        {
            Debug.Log("Target is in view!");
            return true;
        }
        else
        {
            // Debug.Log("Target is not in view.");
            return false;
        }
    }

    private bool IsInView(Vector3 worldPosition, Camera camera)
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(worldPosition);
        bool isInView = viewportPoint.z > 0 &&
                        viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                        viewportPoint.y >= 0 && viewportPoint.y <= 1;
        return isInView;
    }
}
