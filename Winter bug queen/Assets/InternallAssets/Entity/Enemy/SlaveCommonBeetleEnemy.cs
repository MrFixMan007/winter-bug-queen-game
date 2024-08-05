using Zenject;

public class SlaveCommonBeetleEnemy : BeetleEnemy
{
    [Inject]
    protected override void Constructor([Inject(Id = "SlaveCommonBeetleConfig")] EntityConfig beetleConfig)
    {
        base.Constructor(beetleConfig);
    }
}