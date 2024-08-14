using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EntityMovemableGameobject: MonoBehaviour, ITargetable, IRotateableGameObject, IRotateable, IMoveable
{
    [SerializeField] protected CharacterController _characterController;
    [SerializeField] protected GravityMovement _gravityMovement;
    [SerializeField] protected EntityForwardMovement _entityForwardMovement;

    public Transform Position => transform;
    public IDamageable Health;
    
    protected virtual void Constructor(EntityConfig entityConfig, LayerMask enemyLayerMask)
    {
        _entityForwardMovement = new EntityForwardMovement(
            rotateSpeed: entityConfig.RotationSpeed, moveSpeed: entityConfig.MoveSpeed, runSpeed: entityConfig.RunSpeed,
            characterController: _characterController, rotateable: this, targetable: this);
        _gravityMovement = new GravityMovement(entityConfig.GravityForce, _characterController);
        Health = new SimpleHealthSystem(entityConfig.MaxHp);
    }
    
    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    protected void Update()
    {
        _gravityMovement.Fall();
    }

    public void Rotate(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }

    public void Rotate(Vector3 rotateDirection)
    {
        _entityForwardMovement.Rotate(rotateDirection: rotateDirection);
    }

    public void Move(Vector3 moveDirection)
    {
        _entityForwardMovement.Move(moveDirection: moveDirection);
    }

    public void SetRun(bool isRunning)
    {
        _entityForwardMovement.SetRun(isRunning: isRunning);
    }
}