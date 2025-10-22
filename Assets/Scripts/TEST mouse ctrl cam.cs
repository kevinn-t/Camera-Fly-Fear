using UnityEngine;

/* TODO:
-- test speeds
-- get playtest feedback on the speeds
*/

public class TESTmousectrlcam : MonoBehaviour
{
    [Header("Chase / Tracking")] 
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float proximity = 2f;
    [Header("FOV")]
    [SerializeField] private float baseFOV = 60f;
    [SerializeField] private float chaseFOV = 90f;


    Transform target;

    void Update()
    {
        Debug.Log("FOV: " + Camera.main.fieldOfView);

        // switch when LMB is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                target = hit.collider.gameObject.transform.parent.Find("Camera Position");
        }

        if (target)
        {
            // move n turn toward target
            Vector3 dir = (target.position - transform.position).normalized;
            if (dir != Vector3.zero && Vector3.Distance(transform.position, target.position) > proximity)
            {
                Camera.main.fieldOfView = chaseFOV;

                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);

                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            // but if too close, just snap to a set position
            else
            {
                Camera.main.fieldOfView = baseFOV;
                transform.position = target.position;
                transform.rotation = target.rotation;
            }
        }
    }
}
