using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckpointManager.SetCheckpoint(transform.position);
            Debug.Log("Checkpoint updated to: " + transform.position);
        }
    }
}
