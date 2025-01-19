using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    // Center to player
    // [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    // [SerializeField, Range(0f, 1f)] private float smoothTime = 0.125f;

    // Center to left
    [SerializeField] private Vector3 offset = new Vector3(4.59f, 1.5f, -10f);
    [SerializeField, Range(0f, 1f)] private float smoothTime = 0f;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    public void SetTarget(Transform newTarget) => target = newTarget;
}