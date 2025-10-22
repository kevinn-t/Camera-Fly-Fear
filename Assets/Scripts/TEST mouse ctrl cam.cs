using UnityEngine;

/* TODO:
-- shakes violently when too close
*/

public class TESTmousectrlcam : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 6f;
    [SerializeField] private float moveSpeed = 10f;

    Transform target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.collider.gameObject.transform.parent.Find("Camera Position");
            }
        }

        if (target)
        {
            // Turn toward target
            Vector3 dir = (target.position - transform.position).normalized;
            if (dir != Vector3.zero || Vector3.Distance(transform.position, target.position) > 0.3f)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
            }
            else
            {
                transform.LookAt(target.transform.parent.Find("Camera Target"));
            }

            // Move forward
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
