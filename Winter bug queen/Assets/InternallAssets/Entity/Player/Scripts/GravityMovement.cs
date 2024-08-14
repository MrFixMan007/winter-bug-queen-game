using UnityEngine;

[System.Serializable]
public class GravityMovement : IGravityFallable
{
    private readonly CharacterController _characterController;
    [SerializeField]
    private float _currentAttractionCharacter = 0;
    [SerializeField]
    private float _gravityForce;

    public GravityMovement(float gravityForce, CharacterController characterController)
    {
        _gravityForce = gravityForce;
        _characterController = characterController;
    }

    public void Fall()
    {
        if (!_characterController.isGrounded)
        {
            _currentAttractionCharacter -= _gravityForce * Time.deltaTime;
            _characterController.Move(new Vector3(0, _currentAttractionCharacter, 0) * Time.deltaTime);
        }
        else
        {
            _currentAttractionCharacter = 0;
        }
    }

    public float GravityScale
    {
        get => _gravityForce;
        set => _gravityForce = value;
    }
}