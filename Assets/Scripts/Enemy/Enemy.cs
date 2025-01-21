using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public EnemyPatrol enemyPatrol;
    public GameObject enemy01Prefab;
    public Transform enemyTransform;

    // Properties
    public float speed = 2f;
    public float health = 100f; 
    public float damage = 10f;
    [SerializeField] private float enemyCount = 0f;
    [SerializeField] private float maxEnemies = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    void Update()
    {
        // SpawnEnemy();
        EnemyPatrol();
        // DetectPlayer();
        EnemyAttack();
    }

    // ** ENEMY MOVEMENTS **
    private void EnemyPatrol()
    {
        if (enemyCount > 0)
        {
           enemyPatrol.Patrol();
        }
    }

    // ** ENEMY COLLISIONS **
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if enemy is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;

        // Destroy enemy if health is less than or equal to 0
        if (health <= 0)
        {
            Debug.Log("Enemy destroyed");
            enemyCount--;
            Destroy(gameObject);
        }
    }

    //* ENEMY ANIMATIONS **
    private void EnemyAnimator()
    {
        // Implement enemy animation
    }

    // ** ENEMY ACTIONS **
    private void DetectPlayer()
    {
        // Implement player detection
    }

    private void EnemyAttack()
    {
        // Implement enemy attack
    }

    private void EnemySpecial()
    {
        // Implement enemy special attack
    }

    private void SpawnEnemy()
    {
        if (enemyCount < maxEnemies)
        {
            float randomX = Random.Range(-10f, 10f);
            Vector2 spawnPosition = new(randomX, enemyTransform.position.y);


            GameObject enemy01 = Instantiate(
                enemy01Prefab,
                spawnPosition,
                Quaternion.identity
            );
            
            enemyCount++;

            // Set Enemy position
            Enemy enemy = enemy01.GetComponent<Enemy>();
            enemy.transform.position = spawnPosition;
        }
    }
}
