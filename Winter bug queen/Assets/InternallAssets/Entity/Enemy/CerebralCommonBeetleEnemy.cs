using UnityEngine;
using Zenject;

public class CerebralCommonBeetleEnemy : BeetleEnemy
{
    [Inject]
    public void Constructor([Inject(Id = "CerebralCommonBeetleConfig")] EntityConfig beetleConfig)
    {
        EntityMovement = new EntityForwardMovement(beetleConfig.RotationSpeed, beetleConfig.MoveSpeed, beetleConfig.RunSpeed, gameObject);
        _gravityMovement = new GravityMovement(beetleConfig.GravityForce, _characterController);
        Health = new SimpleHealthSystem(beetleConfig.MaxHp);
    }
}