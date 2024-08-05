using UnityEngine;

public class EntityForwardMovement : IMoveable, IRotateable
{
    public float RotateSpeed;

    public float MoveSpeed;
    public float RunSpeed;

    private bool _isRunning;

    private readonly CharacterController _characterController;
    private readonly GameObject _playerObject;

    public EntityForwardMovement(float rotateSpeed, float moveSpeed, float runSpeed, GameObject gameObject)
    {
        RotateSpeed = rotateSpeed;
        MoveSpeed = moveSpeed;
        RunSpeed = runSpeed;
        _characterController = gameObject.GetComponent<CharacterController>();
        _playerObject = gameObject;
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
        if (Vector3.Angle(_playerObject.transform.forward, rotateDirection) > 0)
        {
            Vector3 newDirection =
                Vector3.RotateTowards(_playerObject.transform.forward, rotateDirection, RotateSpeed, 0);
            _playerObject.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}