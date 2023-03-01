using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    // Move settings
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius;

    private bool _isGrounded;

    private Vector2 _moveInput;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;
    
    // Start is called before the first frame update
    
    
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }
    
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (!_isGrounded) return;
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions.FindAction("Movement").performed += OnMovePerformed;
        _playerInput.actions.FindAction("Movement").canceled += OnMoveCanceled;
        _playerInput.actions.FindAction("Jump").performed += OnJumpPerformed;
        
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody is null)
        {
            Debug.LogError("Rigidbody2D is null!");
        } 

    }

    private void Start()
    {
        UnityEngine.Debug.Log($"Hello! My speed is {_speed}");
        UnityEngine.Debug.Log(Vector2.up);
    }
    
    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        float moveX = _moveInput.x * _speed;
        _rigidbody.velocity = new Vector2(moveX, _rigidbody.velocity.y);

        
        transform.localScale = moveX switch
        {
            > 0 => new Vector2(1, 1),
            < 0 => new Vector2(-1, 1),
            _ => transform.localScale
        };
    }
}
