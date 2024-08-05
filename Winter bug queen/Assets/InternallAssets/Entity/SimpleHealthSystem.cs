public class SimpleHealthSystem : IDamageable
{
    private float _hp;
    private float _maxHp;
    public float Hp => _hp;

    public SimpleHealthSystem(float hp, float maxHp)
    {
        _hp = hp;
        _maxHp = maxHp;
    }

    public SimpleHealthSystem(float maxHp)
    {
        _hp = maxHp;
        _maxHp = maxHp;
    }

    public void Hit(float hp)
    {
        _hp -= hp;
        if (_hp < 0) _hp = 0;
    }

    public void Heal(float hp)
    {
        _hp += hp;
        if (_hp > _maxHp) _hp = _maxHp;
    }
}