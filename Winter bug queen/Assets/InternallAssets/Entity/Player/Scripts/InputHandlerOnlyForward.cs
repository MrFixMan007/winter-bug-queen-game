using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandlerOnlyForward : ITickable, IDisposable, IInput
{
    private PlayerInputActions _playerControls;
    private Vector2 _moveDirection;

    private IMoveable _moveablePlayer;
    private IRotateable _rotateablePlayer;
    private ICombat _combat;

    private PlayerMovementDinamicTreeTwoDimensionAnimation _playerMovementAnimation;

    private InputAction _move;
    private InputAction _shift;
    private InputAction _mouse;

    private bool _shiftPressed;
    private bool _forwardPressed;

    [Inject]
    private void Construct(IMoveable moveable, IRotateable rotateable,
        PlayerMovementDinamicTreeTwoDimensionAnimation playerMovementAnimation, ICombat combat)
    {
        _moveablePlayer = moveable;
        _rotateablePlayer = rotateable;
        _combat = combat;
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

        _mouse = _playerControls.Player.Fire;
        _mouse.performed += OnMouseClick;
        _mouse.Enable();
    }

    private void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveDirection = callbackContext.ReadValue<Vector2>();
        if (_moveDirection.x != 0 || _moveDirection.y != 0)
        {
            _forwardPressed = true;
        }
        else
        {
            _forwardPressed = false;
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
    
    private void OnMouseClick(InputAction.CallbackContext callbackContext)
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (_combat.LightAttack())
            {
                Debug.Log("Ударил слегка");
            }
            else
            {
                Debug.Log("Устал, надо подождать");
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (_combat.StrongAttack())
            {
                Debug.Log("Ударил потяжелее");
            }
            else
            {
                Debug.Log("Устал очень, надо подождать");
            }
        }
    }

    public void Tick()
    {
        _moveablePlayer.Move(
            moveDirection: new Vector3(_moveDirection.x, 0, _moveDirection.y));
        _rotateablePlayer.Rotate(rotateDirection: new Vector3(_moveDirection.x, 0,
            _moveDirection.y));

        _playerMovementAnimation.RunPressed = _shiftPressed;
        _playerMovementAnimation.ForwardPressed = _forwardPressed;
    }

    public void Dispose()
    {
        _move.Disable();
        _shift.Disable();
        _mouse.Disable();
    }
}