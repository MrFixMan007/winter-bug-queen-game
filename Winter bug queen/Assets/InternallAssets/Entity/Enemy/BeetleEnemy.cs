using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BeetleEnemy : MonoBehaviour, ITargetable
{
    [SerializeField] protected CharacterController _characterController;

    protected IGravityFallable _gravityMovement;
    public EntityForwardMovement EntityMovement { get; protected set; }

    public Transform Position => transform;
    public IDamageable Health;
    
    protected virtual void Constructor(EntityConfig beetleConfig)
    {
        EntityMovement = new EntityForwardMovement(beetleConfig.RotationSpeed, beetleConfig.MoveSpeed, beetleConfig.RunSpeed, gameObject);
        _gravityMovement = new GravityMovement(beetleConfig.GravityForce, _characterController);
        Health = new SimpleHealthSystem(beetleConfig.MaxHp);
    }

    protected void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    protected void Update()
    {
        _gravityMovement.Fall();
    }
}