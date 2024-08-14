using UnityEngine;
[System.Serializable]

public class EntityForwardMovement : IMoveable, IRotateable
{
    public float MoveSpeed;
    public float RunSpeed;
    public float RotateSpeed;

    [SerializeField]
    private bool _isRunning;

    private readonly CharacterController _characterController;
    private readonly IRotateableGameObject _entityRotation;
    private readonly ITargetable _entityPosition;

    public EntityForwardMovement(float rotateSpeed, float moveSpeed, float runSpeed,
        CharacterController characterController, IRotateableGameObject rotateable, ITargetable targetable)
    {
        RotateSpeed = rotateSpeed;
        MoveSpeed = moveSpeed;
        RunSpeed = runSpeed;
        _characterController = characterController;
        _entityRotation = rotateable;
        _entityPosition = targetable;
    }

    public void Move(Vector3 moveDirection)
    {
        if (_isRunning)
            moveDirection *= RunSpeed;
        else
            moveDirection *= MoveSpeed;
        _characterController.Move(moveDirection * Time.deltaTime);
    }

    public void SetRun(bool isRunning)
    {
        _isRunning = isRunning;
    }

    public void Rotate(Vector3 rotateDirection)
    {
        if (Vector3.Angle(_entityPosition.Position.transform.forward, rotateDirection) > 0)
        {
            Vector3 newDirection =
                Vector3.RotateTowards(_entityPosition.Position.transform.forward, rotateDirection, RotateSpeed, 0);
            _entityRotation.Rotate(Quaternion.LookRotation(newDirection));
        }
    }
}