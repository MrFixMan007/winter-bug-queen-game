using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, ITargetable
{
    [SerializeField] private CharacterController _characterController;

    private IGravityFallable _gravityMovement;
    private InputHandler _inputHandler;
    private EntityMovement _entityMovement;

    public Transform Position => transform;

    private void Awake()
    {
        _inputHandler = new InputHandler();
    }

    [Inject]
    public void Constructor(PlayerConfig playerConfig)
    {
        _entityMovement = new EntityMovement(playerConfig.RotationSpeed, playerConfig.MoveSpeed, gameObject);
        _gravityMovement = new GravityMovement(playerConfig.GravityForce, _characterController);
    }

    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    private void Update()
    {
        _gravityMovement.Fall();
        Move();
    }

    private void Move()
    {
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