using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    private bool _bulletSpray;

    void Update()
    {
        if (_bulletSpray)
        {
            FireBullet();
        }      
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();       
        rb.velocity = _bulletSpeed * transform.right;
    }

    private void OnFire(InputValue inputValue)
    {
        _bulletSpray = inputValue.isPressed;
    }
}
