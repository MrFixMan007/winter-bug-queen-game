using UnityEngine;
using Zenject;

public class CerebralCommonBeetleEnemy : BeetleEnemy
{
    [Inject]
    protected override void Constructor([Inject(Id = "CerebralCommonBeetleConfig")] EntityConfig beetleConfig,
        [Inject(Id = "PlayerLayerMask")] LayerMask playerLayerMask)
    {
        base.Constructor(entityConfig: beetleConfig, enemyLayerMask: playerLayerMask);
    }
}