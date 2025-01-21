using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float waitTime = 1f;

    private int _currentWaypoint = 0;
    private bool _isWaiting = false;

    void Update()
    {
        if (!_isWaiting)
        {
            Patrol();
        }
    }

    public void Patrol()
    {
        Transform targetWaypoint = waypoints[_currentWaypoint];
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetWaypoint.position,
            speed * Time.deltaTime
        );
        
        // Flip the sprite to face the next waypoint
        Vector2 direction = targetWaypoint.position - transform.position;
        if (direction.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        
        // Check if enemy is at target point
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
        _isWaiting = false;
    }
}