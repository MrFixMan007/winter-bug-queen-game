using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : ITickable, IDisposable, IInitializable
{
    private PlayerInputActions _playerControls;
    private Vector2 _moveDirection;
    private IMoveable _moveablePlayer;
    private IRotateable _rotateablePlayer;

    private InputAction _move;

    [Inject]
    private void Construct(IMoveable moveable, IRotateable rotateable)
    {
        _moveablePlayer = moveable;
        _rotateablePlayer = rotateable;
        _playerControls = new PlayerInputActions();
        EnableActions();
    }

    public void EnableActions()
    {
        _move = _playerControls.Player.Move;
        _move.performed += OnMove;
        _move.canceled += OnMove;
        _move.Enable();
    }

    private void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveDirection = callbackContext.ReadValue<Vector2>();
    }

    public void Tick()
    {
        _moveablePlayer.Move(
            moveDirection: new Vector3(_moveDirection.x, 0, _moveDirection.y));
        _rotateablePlayer.Rotate(rotateDirection: new Vector3(_moveDirection.x, 0,
            _moveDirection.y));
    }

    public void Dispose()
    {
        _move.Disable();
    }

    public void Initialize()
    {
        _playerControls = new PlayerInputActions();
    }
}