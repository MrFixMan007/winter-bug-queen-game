using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, ITargetable
{
    [SerializeField] private CharacterController _characterController;

    private IGravityFallable _gravityMovement;
    public EntityForwardMovement EntityMovement { get; private set; }

    public Transform Position => transform;
    public IDamageable Health;

    [Inject]
    public void Constructor([Inject(Id = "PlayerConfig")] EntityConfig playerConfig)
    {
        EntityMovement = new EntityForwardMovement(playerConfig.RotationSpeed, playerConfig.MoveSpeed, playerConfig.RunSpeed, gameObject);
        _gravityMovement = new GravityMovement(playerConfig.GravityForce, _characterController);
        Health = new SimpleHealthSystem(playerConfig.MaxHp);
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