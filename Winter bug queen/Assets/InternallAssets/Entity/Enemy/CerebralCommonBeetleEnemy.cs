using Zenject;

public class CerebralCommonBeetleEnemy : BeetleEnemy
{
    [Inject]
    protected override void Constructor([Inject(Id = "CerebralCommonBeetleConfig")] EntityConfig beetleConfig)
    {
        base.Constructor(beetleConfig);
    }
}