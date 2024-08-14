using UnityEngine;

[System.Serializable]
public class PlayerCombat : ICombat
{
    [Header("Light Attack settings")]
    [SerializeField] private float _lightReloadTime;
    [SerializeField] private float _lightAttackRange;
    [SerializeField] private float _damageLight;
    [Space]
    [SerializeField] private bool _canHitLight = true;

    [Header("Strong Attack settings")]
    [SerializeField] private float _strongReloadTime;
    [SerializeField] private float _strongAttackRange;
    [SerializeField] private float _strongAttackDistance;
    [SerializeField] private float _damageStrong;
    [Space]
    [SerializeField] private bool _canHitStrong = true;

    [Header("Other settings")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayerMask;

    private float _lightAttackTick;
    private float _strongAttackTick;

    public PlayerCombat(float lightReloadTime, float lightAttackRange, float damageLight, float strongReloadTime,
        float strongAttackRange, float strongAttackDistance, float damageStrong, Transform attackPoint,
        LayerMask enemyLayerMask)
    {
        _lightReloadTime = lightReloadTime;
        _lightAttackRange = lightAttackRange;
        _damageLight = damageLight;
        _strongReloadTime = strongReloadTime;
        _strongAttackRange = strongAttackRange;
        _strongAttackDistance = strongAttackDistance;
        _damageStrong = damageStrong;
        _attackPoint = attackPoint;
        _enemyLayerMask = enemyLayerMask;
        //TODO: предполагается что перезарядка будет завязана на оружии
    }

    private void ReloadTick()
    {
        if (!_canHitLight)
        {
            _lightAttackTick += Time.deltaTime;
            if (_lightAttackTick >= _lightReloadTime)
            {
                _lightAttackTick = 0;
                _canHitLight = true;
            }
        }

        if (!_canHitStrong)
        {
            _strongAttackTick += Time.deltaTime;
            if (_strongAttackTick >= _strongReloadTime)
            {
                _strongAttackTick = 0;
                _canHitStrong = true;
            }
        }
    }

    public bool LightAttack()
    {
        if (_canHitLight)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _lightAttackRange, _enemyLayerMask);
            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("enemy was hit");
                enemy.GetComponent<BeetleEnemy>().Health.Hit(_damageLight);
            }

            _canHitLight = false;
            return true;
        }

        return false;
    }

    public bool StrongAttack()
    {
        if (_canHitStrong)
        {
            Collider[] hitEnemies = Physics.OverlapCapsule(_attackPoint.position,
                new Vector3(_attackPoint.position.x, _attackPoint.position.y,
                    _attackPoint.position.z + _strongAttackDistance),
                _strongAttackRange, _enemyLayerMask);
            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("enemy was hit strong");
                enemy.GetComponent<BeetleEnemy>().Health.Hit(_damageStrong);
            }

            _canHitStrong = false;
            return true;
        }

        return false;
    }

    public void AddReloadTick()
    {
        ReloadTick();
    }
}