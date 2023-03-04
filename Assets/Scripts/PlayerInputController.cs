using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private IMovable _movable;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.PlayerActionMap.Enable();

        _movable = GetComponent<IMovable>();

        if (_movable is null)
        {
            throw new Exception($"There is no IMovable on the object: {gameObject.name}");
        }
    }

    private void OnEnable()
    {
        _playerInput.PlayerActionMap.Jump.performed += OnJumpPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        _movable.Jump();
    }

    private void OnDisable()
    {
        _playerInput.PlayerActionMap.Jump.performed -= OnJumpPerformed;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ReadMovement()
    {
        var inputDirection = _playerInput.PlayerActionMap.Movement.ReadValue<Vector2>();
        var direction = new Vector3(inputDirection.x, 0f, 0f);
        if (direction != Vector3.zero)
            _movable.Move(direction);
    }
    
    private void Update()
    {
        ReadMovement();
    }
}
