using UnityEngine;

public class BirdFlight : MonoBehaviour
{
    [Header("Flight Settings")]
    public float speed = 5f;               // Move speed
    public float turnSpeed = 2f;           // Turn speed
    public float pathChangeInterval = 3f;  // Time before picking new spot
    public float flightRange = 25f;        // How far birds can fly
    public float minHeight = 2f;           // Lowest height
    public float maxHeight = 10f;          // Highest height

    private Vector3 targetPoint;

    void Start()
    {
        PickNewTarget();
        InvokeRepeating(nameof(PickNewTarget), pathChangeInterval, pathChangeInterval);
    }

    void Update()
    {
        // Turn toward target
        Vector3 dir = (targetPoint - transform.position).normalized;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
        }

        // Move forward
        transform.position += transform.forward * speed * Time.deltaTime;

        // Keep above ground
        if (transform.position.y < minHeight)
        {
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
        }

        // Get new target if close
        if (Vector3.Distance(transform.position, targetPoint) < 1f)
        {
            PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        // Pick random position
        targetPoint = new Vector3(
            Random.Range(-flightRange, flightRange),
            Random.Range(minHeight, maxHeight),
            Random.Range(-flightRange, flightRange)
        );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPoint, 0.5f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, targetPoint);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
