using UnityEngine;
using Zenject;

public class SlaveCommonBeetleEnemy : BeetleEnemy
{
    [Inject]
    protected override void Constructor([Inject(Id = "SlaveCommonBeetleConfig")] EntityConfig beetleConfig,
        [Inject(Id = "PlayerLayerMask")] LayerMask playerLayerMask)
    {
        base.Constructor(entityConfig: beetleConfig, enemyLayerMask: playerLayerMask);
    }
}