using UnityEngine;
using Zenject;

public class SlaveCommonBeetleEnemy : BeetleEnemy
{
    [Inject]
    public void Constructor([Inject(Id = "SlaveCommonBeetleConfig")] EntityConfig beetleConfig)
    {
        EntityMovement = new EntityForwardMovement(beetleConfig.RotationSpeed, beetleConfig.MoveSpeed, beetleConfig.RunSpeed, gameObject);
        _gravityMovement = new GravityMovement(beetleConfig.GravityForce, _characterController);
        Health = new SimpleHealthSystem(beetleConfig.MaxHp);
    }
}