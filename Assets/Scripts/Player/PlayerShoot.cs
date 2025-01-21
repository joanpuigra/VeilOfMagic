using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _bulletSpeed;

    [SerializeField] private Transform _firePoint;

    [SerializeField] private float _fireRate;

    private bool _bulletSpray;
    private float _lastFireTime;

    void Update()
    {
        if (_bulletSpray)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;
            
            if (timeSinceLastFire >= _fireRate)
            {
                FireBullet();
                _lastFireTime = Time.time;
            }
        }      
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();       
        rb.velocity = _bulletSpeed * transform.right;
    }

    private void OnFire(InputValue inputValue)
    {
        _bulletSpray = inputValue.isPressed;
    }
}
