using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BeetleEnemy : EntityMovemableGameobject
{
    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }
}