using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private GameObject _pistolActive;

    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector2 _movementInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("isIdle", true);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementInput * _speed;
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();

        if (_movementInput.y > 0)
        {
            _movementInput.y = 0;
        }

        if (_movementInput != Vector2.zero)
        {
            _animator.SetBool("isIdle", false);
            if (_movementInput.x > 0)
            {
                _animator.SetBool("isWalkingRight", true);
                // _pistolActive.SetActive(_pistolPrefabRight);
            }
            else if (_movementInput.x < 0)
            {
                _animator.SetBool("isWalkingLeft", true);
                // _pistolActive.SetActive(_pistolPrefabLeft);
            }
        }
        else
        {
            _animator.SetBool("isWalkingRight", false);
            _animator.SetBool("isWalkingLeft", false);
            _animator.SetBool("isIdle", true);
            // _pistolActive.SetActive(false);
        }
    }
}
