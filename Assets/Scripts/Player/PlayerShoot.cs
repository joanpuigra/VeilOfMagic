using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private Transform _firePoint;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !_player.HasAmmo) return;
        // FlipTowardsMouse();
        FireBullet();
        _player.UseAmmo();
    }
    
    private void FlipTowardsMouse()
    {
        if (Camera.main == null) return;
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDirection = mouseWorldPos - transform.position;

        Vector3 scale = transform.localScale;
        scale.x = lookDirection.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void FireBullet()
    {
        if (Camera.main == null) return;
        
        // Get the mouse position in world coordinates
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)_firePoint.position).normalized;

        // Create the bullet at the fire point with the calculated direction
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        if (bullet.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = direction * _bulletSpeed;
        }

        // Rotate the buller if it has direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}