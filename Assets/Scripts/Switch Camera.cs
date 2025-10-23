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

    private bool isChasing = false;


    Transform target;

    void Update()
    {

        // switch when LMB is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                try
                {
                    target = hit.collider.gameObject.transform.parent.Find("Camera Position");
                }
                catch
                {
                    Debug.Log("[IGNORE] clicked on non-bird");
                }
        }

        if (target)
        {
            // move n turn toward target
            Vector3 dir = (target.position - transform.position).normalized;
            if (dir != Vector3.zero && Vector3.Distance(transform.position, target.position) > proximity)
            {
                isChasing = true;
                Camera.main.fieldOfView = chaseFOV;

                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);

                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            // but if too close, just snap to a set position
            else
            {
                if (isChasing)
                {
                    Camera.main.fieldOfView = baseFOV;
                }
                isChasing = false;
                transform.position = target.position;
                if (Input.GetKey(KeyCode.Space))
                {
                    transform.rotation = target.rotation;
                } else
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                }
                
            }
        }
    }
}
