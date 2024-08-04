using UnityEngine;

public class GravityMovement : IGravityFallable
{
    private CharacterController _characterController;
    private float _currentAttractionCharacter = 0;
    public float GravityForce;

    public GravityMovement(float gravityForce)
    {
        GravityForce = gravityForce;
    }

    public void Fall()
    {
        if (!_characterController.isGrounded)
        {
            _currentAttractionCharacter -= GravityForce * Time.deltaTime;
            _characterController.Move(new Vector3(0, _currentAttractionCharacter, 0) * Time.deltaTime);
        }
        else
        {
            _currentAttractionCharacter = 0;
        }
    }

    public void SetCharacterController(CharacterController characterController)
    {
        if (_characterController == null)
        {
            _characterController = characterController;
        }
    }
}
