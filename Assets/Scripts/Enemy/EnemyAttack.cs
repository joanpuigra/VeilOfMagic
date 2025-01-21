using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    private Vector2 attackPoint;
    public Transform player;

    public bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        OnAttack();
    }


    private void OnAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                isAttacking = true;
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else
            {
                isAttacking = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }
}
