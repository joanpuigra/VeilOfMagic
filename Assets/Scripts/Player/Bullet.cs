using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(damage, transform.position);
            }

            Destroy(gameObject);
        }
    }
}
