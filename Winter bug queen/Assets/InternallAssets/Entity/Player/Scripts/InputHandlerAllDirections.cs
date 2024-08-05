using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandlerAllDirections : ITickable, IDisposable, IInput
{
    private PlayerInputActions _playerControls;
    private Vector2 _moveDirection;
    
    private IMoveable _moveablePlayer;
    
    private PlayerMovementDinamicTreeTwoDimensionAnimation _playerMovementAnimation;

    private InputAction _move;
    private InputAction _shift;

    private bool _shiftPressed;
    private bool _forwardPressed;
    private bool _backPressed;
    private bool _leftPressed;
    private bool _rightPressed;

    [Inject]
    private void Construct(IMoveable moveable, PlayerMovementDinamicTreeTwoDimensionAnimation playerMovementAnimation)
    {
        _moveablePlayer = moveable;
        _playerControls = new PlayerInputActions();
        _playerMovementAnimation = playerMovementAnimation;
        EnableActions();
    }

    public void EnableActions()
    {
        _move = _playerControls.Player.Move;
        _move.performed += OnMove;
        _move.canceled += OnMove;
        _move.Enable();

        _shift = _playerControls.Player.Run;
        _shift.performed += OnShiftPressed;
        _shift.canceled += OnShiftCanceled;
        _shift.Enable();
    }

    private void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveDirection = callbackContext.ReadValue<Vector2>();
        if (_moveDirection.x > 0)
        {
            _rightPressed = true;
            _leftPressed = false;
        }
        else if (_moveDirection.x < 0)
        {
            _rightPressed = false;
            _leftPressed = true;
        }
        else
        {
            _rightPressed = false;
            _leftPressed = false;
        }
        
        if (_moveDirection.y > 0)
        {
            _forwardPressed = true;
            _backPressed = false;
        }
        else if (_moveDirection.y < 0)
        {
            _forwardPressed = false;
            _backPressed = true;
        }
        else
        {
            _forwardPressed = false;
            _backPressed = false;
        }
    }

    private void OnShiftPressed(InputAction.CallbackContext callbackContext)
    {
        _shiftPressed = true;
        _moveablePlayer.SetRun(_shiftPressed);
    }
    
    private void OnShiftCanceled(InputAction.CallbackContext callbackContext)
    {
        _shiftPressed = false;
        _moveablePlayer.SetRun(_shiftPressed);
    }

    public void Tick()
    {
        _moveablePlayer.Move(
            moveDirection: new Vector3(_moveDirection.x, 0, _moveDirection.y));

        _playerMovementAnimation.RunPressed = _shiftPressed;
        _playerMovementAnimation.RightPressed = _rightPressed;
        _playerMovementAnimation.LeftPressed = _leftPressed;
        _playerMovementAnimation.ForwardPressed = _forwardPressed;
        _playerMovementAnimation.BackPressed = _backPressed;
    }

    public void Dispose()
    {
        _move.Disable();
        _shift.Disable();
    }
}