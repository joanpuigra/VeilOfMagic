using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");

    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector2 _movementInput;
    private float _idleTimer;
    public bool isDead;
    private Player _player;
    private PlayerClimb _playerClimb;
    
    [Header("Jumping")]
    [SerializeField] private float _jumpForce = 10f;
    private bool _isJumping;
    private bool _isWallSliding;
    [SerializeField] private float _wallSlidingSpeed = 2f;
    [SerializeField] private Transform _wallCheck;
    
    public float moveValue { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerClimb = GetComponent<PlayerClimb>();
        _animator.SetFloat(Speed, 0f);
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movementInput.x * _speed, _rb.velocity.y);
        WallSlide();
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

    private bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(_wallCheck.position, 0.2f);
    }

    private void OnJump(InputValue inputValue)
    {
        if (!inputValue.isPressed || _isJumping) return;

        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _isJumping = true;
    }

    private void OnMove(InputValue inputValue)
    {
        if (_playerClimb != null && _playerClimb.isClimbing)
        {
            moveValue = 0f;
            return;
        }

        moveValue = Input.GetAxisRaw("Horizontal");
        
        if (isDead) return;

        _movementInput = inputValue.Get<Vector2>();
        moveValue = _movementInput.x;

        bool isMoving = moveValue != 0f;

        if (isMoving)
        {
            _animator.SetFloat(Speed, 0f);
            
            if (moveValue > 0f) { _animator.SetFloat(Speed, 1f); }
            else if (moveValue < 0f) { _animator.SetFloat(Speed, -1f); }
        }
        else
        {
            _animator.SetFloat(Speed, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }
}
