using UnityEngine;

public class Player : MonoBehaviour
{
    // GameObjects
    private Animator animator;
    private Vector2 input = new();
    private Rigidbody2D rb;

    // Properties
    private float speed = 5f;
    private float jumpForce = 5f;

    // Spells
    public GameObject FireBallPrefab;
    public Transform FireBall;

    private float health = 100f;
    private float mana = 100f;
    // private List<string> spells = new List<string>();
    // private List<string> items = new List<string>();

    // Conditions
    private bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Player movements
        MovePlayer();
        PlayerAnimator();

        // Player status
        PlayerStats();
        PlayerSpells();

        // Player actions
        JumpPlayer();
        PlayerAttack();
    }

    // ** PLAYER MOVEMENTS **
    // Player movements
    private void MovePlayer()
    {
        input.x = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(input.x, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    // ** PLAYER COLLISIONS **
    // Collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is on the ground
        //! FIXME: Collision on the sides of the player will trigger isJumping = false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    // ** PLAYER ANIMATIONS **
    private void PlayerAnimator()
    {
        // Implement player animation logic here
    }

    // ** PLAYER STATS **
    private void PlayerStats()
    {
        // Implement player stats logic here
    }

    // ** PLAYER SPELLS **
    private void PlayerSpells()
    {
        // Implement player spells logic here
    }

    // ** PLAYER ACTIONS **
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Mouse Position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // FireBall Direction
            Vector2 fireBallDirection = FireBall.position;

            // Set FireBall direction from mouse position
            Vector2 direction = (mousePosition - fireBallDirection).normalized;

            // Instantiate FireBall
            GameObject fireBall = Instantiate(FireBallPrefab, FireBall.position, Quaternion.identity);

            // Set FireBall direction
            FireBall fb = fireBall.GetComponent<FireBall>();
            fb.SetDirection(direction);
        }
    }
}
