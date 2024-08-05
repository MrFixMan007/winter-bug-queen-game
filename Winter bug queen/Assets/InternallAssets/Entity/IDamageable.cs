public interface IDamageable
{
    void Hit(float hp);
    void Heal(float hp);
    float Hp { get; }
}