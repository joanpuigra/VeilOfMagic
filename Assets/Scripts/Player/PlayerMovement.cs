using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector2 _movementInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementInput * _speed;
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();

        if (_movementInput != Vector2.zero)
        {
            if (_movementInput.x > 0)
            {
                _animator.SetBool("isWalkingRight", true);
            }
            else if (_movementInput.x < 0)
            {
                _animator.SetBool("isWalkingLeft", true);
            }
            //_animator.SetBool("isWalkingRight", _movementInput != Vector2.zero);
        }
        else
        {
            _animator.SetBool("isWalkingRight", false);
            _animator.SetBool("isWalkingLeft", false);
            _animator.SetBool("isIdle", true);
        }
    }
}
