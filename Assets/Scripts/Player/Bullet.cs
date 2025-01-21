using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
