using UnityEngine;

public class EntityMovement : IMoveable, IRotateable
{
    public float RotateSpeed;
    // {
    //     get => RotateSpeed;
    //     set
    //     {
    //         if (value > 0)
    //         {
    //             RotateSpeed = value;
    //         }
    //     }
    // }

    public float MoveSpeed;
    // {
    //     get => MoveSpeed;
    //     set
    //     {
    //         if (value > 0)
    //         {
    //             MoveSpeed = value;
    //         }
    //     }
    // }

    private readonly CharacterController _characterController;
    private readonly GameObject _playerObject;

    public EntityMovement(float rotateSpeed, float moveSpeed, GameObject gameObject)
    {
        RotateSpeed = rotateSpeed;
        MoveSpeed = moveSpeed;
        _characterController = gameObject.GetComponent<CharacterController>();
        _playerObject = gameObject;
    }

    public void Move(Vector3 moveDirection)
    {
        moveDirection *= MoveSpeed;
        _characterController.Move(moveDirection * Time.deltaTime);
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