using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector2 _movementInput;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce;
    private bool _isJumping;
    private bool _isWallSliding;
    [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private Transform _wallCheck;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("isIdle", true);
    }

    private void FixedUpdate()
    {
        Vector2 newVelocity = _rb.velocity;
        newVelocity.x = _movementInput.x * _speed;
        _rb.velocity = newVelocity;
        WallSlide();
    }

    private bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(_wallCheck.position, 0.2f);
    }

    private void WallSlide()
    {
        if (IsTouchingWall() && !_isJumping)
        {
            _isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, -_wallSlidingSpeed);
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (!_isJumping)
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _isJumping = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();

        if (_movementInput != Vector2.zero)
        {
            _animator.SetBool("isIdle", false);
            if (_movementInput.x > 0)
            {
                _animator.SetBool("isWalkingRight", true);
                // _weaponActive.SetActive(_pistolPrefabRight);
            }
            else if (_movementInput.x < 0)
            {
                _animator.SetBool("isWalkingLeft", true);
                // _weaponActive.SetActive(_pistolPrefabLeft);
            }
        }
        else
        {
            _animator.SetBool("isWalkingRight", false);
            _animator.SetBool("isWalkingLeft", false);
            _animator.SetBool("isIdle", true);
            // _weaponActive.SetActive(false);
        }
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(_wallCheck.position, 0.2f);
    // }
}
