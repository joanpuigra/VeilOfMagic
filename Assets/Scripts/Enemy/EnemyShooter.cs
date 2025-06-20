using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float detectionRange = 10f;

    private float nextFireTime;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange && Time.time >= nextFireTime)
        {
            FireAtPlayer();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FireAtPlayer()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        if (bullet.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = direction * bulletSpeed;
        }
    }
}