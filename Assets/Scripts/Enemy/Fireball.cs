using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out Player player))
            {
                player.TakeDamage(damage, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
