using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    private IGravityFallable _gravityMovement;
    private InputHandler _inputHandler;
    private EntityMovement _entityMovement;

    private void Awake()
    {
        _entityMovement = new EntityMovement(15, 15, gameObject);
        _inputHandler = new InputHandler();
    }

    [Inject]
    public void Constructor(IGravityFallable iGravityFallable)
    {
        iGravityFallable.SetCharacterController(_characterController);
        _gravityMovement = iGravityFallable;
    }

    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    private void Update()
    {
        _gravityMovement.Fall();
        _entityMovement.Move(
            moveDirection: new Vector3(_inputHandler.MoveDirection.x, 0, _inputHandler.MoveDirection.y));
        _entityMovement.Rotate(rotateDirection: new Vector3(_inputHandler.MoveDirection.x, 0,
            _inputHandler.MoveDirection.y));
    }

    private void OnDestroy()
    {
        _inputHandler.DisableActions();
    }
}