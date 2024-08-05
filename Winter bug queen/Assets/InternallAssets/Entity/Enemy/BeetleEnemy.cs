using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BeetleEnemy : MonoBehaviour, ITargetable
{
    [SerializeField] protected CharacterController _characterController;

    protected IGravityFallable _gravityMovement;
    public EntityForwardMovement EntityMovement { get; protected set; }

    public Transform Position => transform;
    public IDamageable Health;

    protected void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    protected void Update()
    {
        _gravityMovement.Fall();
    }
}