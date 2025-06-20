using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Vector2 LastCheckpointPosition;

    private void Awake()
    {
        LastCheckpointPosition = transform.position;
    }

    public static void SetCheckpoint(Vector2 newPosition)
    {
        LastCheckpointPosition = newPosition;
    }
}
