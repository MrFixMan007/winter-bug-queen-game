using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, ITargetable
{
    [SerializeField] private CharacterController _characterController;

    private IGravityFallable _gravityMovement;
    public EntityMovement EntityMovement { get; private set; }

    public Transform Position => transform;

    [Inject]
    public void Constructor(EntityConfig playerConfig)
    {
        EntityMovement = new EntityMovement(playerConfig.RotationSpeed, playerConfig.MoveSpeed, gameObject);
        _gravityMovement = new GravityMovement(playerConfig.GravityForce, _characterController);
    }

    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    private void Update()
    {
        _gravityMovement.Fall();
    }
}