using UnityEngine;

public class Player : MonoBehaviour
{
    // GameObjects
    private Animator _animator;
    private Vector2 _input = new();
    private Rigidbody2D _rb;

    // Properties
    private readonly float _speed = 5f;
    private readonly float _jumpForce = 5f;
    public float health = 100f;
    public float ammo = 30f;

    // public GameObject bulletPrefab;
    // public Bullet bullet;
    // public GameObject pistol;

    // Conditions
    private bool isJumping = false;
    private bool isWalking = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        // bullet = bulletPrefab.GetComponent<Bullet>();
    }

    private void Update()
    {
        // Player movements
        // MovePlayer();
        PlayerAnimator();

        // Player actions
        JumpPlayer();
        // PlayerAttack();
    }

    // // ** PLAYER MOVEMENTS **
    // private void MovePlayer()
    // {
    //     isWalking = true;
    //     _input.x = Input.GetAxis("Horizontal");

    //     Vector2 movement = _speed * Time.deltaTime * new Vector2(_input.x, 0f);
    //     transform.Translate(movement);
    // }

    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    // ** PLAYER COLLISIONS **
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is on the ground
        //! FIXME: Collision on the sides of the player will trigger isJumping = false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        // Destroy player if health is less than or equal to 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // ** PLAYER ANIMATIONS **
    private void PlayerAnimator()
    {
        // Implement player animation
    }

    // ** PLAYER ACTIONS **
    // private void PlayerAttack()
    // {
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
            // Check if player has ammo
            // if (ammo <= 0) return;
            
            // Reduce Ammo
            // ammo -= 1f;


            // Mouse Position
            // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Bullet Direction
            // Vector2 bulletDirection = transform.position;

            // Set Bullet direction from mouse position
            // Vector2 direction = (mousePosition - bulletDirection).normalized;

            // Instantiate Bullet
            // Instantiate(bulletPrefab, bullet.transform.position, Quaternion.identity);

            // Set Bullet direction
            // bullet.SetDirection(direction);
        // }
    // }
}
