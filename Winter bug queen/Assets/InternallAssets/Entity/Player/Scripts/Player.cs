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
    public PlayerCombat PlayerCombat;

    [Inject]
    public void Constructor([Inject(Id = "PlayerConfig")] EntityConfig playerConfig, [Inject(Id = "EnemyLayerMask")] LayerMask enemyLayerMask)
    {
        EntityMovement = new EntityForwardMovement(playerConfig.RotationSpeed, playerConfig.MoveSpeed,
            playerConfig.RunSpeed, gameObject);
        _gravityMovement = new GravityMovement(playerConfig.GravityForce, _characterController);
        Health = new SimpleHealthSystem(playerConfig.MaxHp);
        PlayerCombat = new PlayerCombat(lightReloadTime: 5, lightAttackRange: 5, damageLight: 5, strongReloadTime: 5,
            strongAttackRange: 4, strongAttackDistance: 5, damageStrong: 10, attackPoint: gameObject.transform,
            enemyLayerMask);
    }

    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    private void Update()
    {
        _gravityMovement.Fall();
        PlayerCombat.AddReloadTick();
    }
}