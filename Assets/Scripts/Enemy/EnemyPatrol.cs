using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentWaypoint = 0;

    void Update()
    {
        Patrol();
    }

    public void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypoint];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;
        transform.Translate(speed * Time.deltaTime * direction);
        
        // Check if enemy is at target point
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}
