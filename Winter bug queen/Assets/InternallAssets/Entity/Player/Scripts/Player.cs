using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class Player : EntityMovemableGameobject, ICombat
{
    [SerializeField]
    public PlayerCombat PlayerCombat;

    [Inject]
    protected override void Constructor([Inject(Id = "PlayerConfig")] EntityConfig playerConfig,
        [Inject(Id = "EnemyLayerMask")] LayerMask enemyLayerMask)
    {
        base.Constructor(entityConfig: playerConfig, enemyLayerMask: enemyLayerMask);

        PlayerCombat = new PlayerCombat(lightReloadTime: 5, lightAttackRange: 5, damageLight: 5, strongReloadTime: 5,
            strongAttackRange: 4, strongAttackDistance: 5, damageStrong: 10, attackPoint: gameObject.transform,
            enemyLayerMask);
    }

    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    private new void Update()
    {
        base.Update();
        PlayerCombat.AddReloadTick();
    }

    public bool LightAttack()
    {
        throw new System.NotImplementedException(); //TODO
    }

    public bool StrongAttack()
    {
        throw new System.NotImplementedException(); //TODO
    }

    public void AddReloadTick()
    {
        throw new System.NotImplementedException(); //TODO
    }
}