using UnityEngine;
using Zenject;

public class PlayerCombat : ICombat
{
    public float LightReloadTime = 2f;
    public float LightAttackRange = 2f;
    private bool _canHitLight = true;
    public float DamageLight = 100;

    public float StrongReloadTime = 5f;
    public float StrongAttackRange = 1f;
    public float StrongAttackDistance = 5f;
    private bool _canHitStrong = true;
    public float DamageStrong = 500;

    public Transform AttackPoint;
    public LayerMask EnemyLayerMask;

    private float _lightAttackTick;
    private float _strongAttackTick;

    public PlayerCombat(float lightReloadTime, float lightAttackRange, float damageLight, float strongReloadTime,
        float strongAttackRange, float strongAttackDistance, float damageStrong, Transform attackPoint,
        LayerMask enemyLayerMask)
    {
        LightReloadTime = lightReloadTime;
        LightAttackRange = lightAttackRange;
        DamageLight = damageLight;
        StrongReloadTime = strongReloadTime;
        StrongAttackRange = strongAttackRange;
        StrongAttackDistance = strongAttackDistance;
        DamageStrong = damageStrong;
        AttackPoint = attackPoint;
        EnemyLayerMask = enemyLayerMask;
        //TODO: предполагается что перезарядка будет завязана на оружии
    }

    private void ReloadTick()
    {
        if (!_canHitLight)
        {
            _lightAttackTick += Time.deltaTime;
            if (_lightAttackTick >= LightReloadTime)
            {
                _lightAttackTick = 0;
                _canHitLight = true;
            }
        }

        if (!_canHitStrong)
        {
            _strongAttackTick += Time.deltaTime;
            if (_strongAttackTick >= StrongReloadTime)
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
            Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, LightAttackRange, EnemyLayerMask);
            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("enemy was hit");
                enemy.GetComponent<BeetleEnemy>().Health.Hit(DamageLight);
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
            Collider[] hitEnemies = Physics.OverlapCapsule(AttackPoint.position,
                new Vector3(AttackPoint.position.x, AttackPoint.position.y,
                    AttackPoint.position.z + StrongAttackDistance),
                StrongAttackRange, EnemyLayerMask);
            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("enemy was hit strong");
                enemy.GetComponent<BeetleEnemy>().Health.Hit(DamageStrong);
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