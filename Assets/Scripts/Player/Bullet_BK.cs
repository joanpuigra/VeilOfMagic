using UnityEngine;

public class Bullet_BK : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;

    private Animator animator;
    private Vector2 direction;
    public float damage = 10f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}