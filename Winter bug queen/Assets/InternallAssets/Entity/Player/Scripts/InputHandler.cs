using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    private PlayerInputActions _playerControls;
    private Vector2 _moveDirection;
    public Vector2 MoveDirection => _moveDirection;
    
    private InputAction _move;

    public InputHandler()
    {
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

    public void DisableActions()
    {
        _move.Disable();
    }

    private void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveDirection = callbackContext.ReadValue<Vector2>();
    }
}