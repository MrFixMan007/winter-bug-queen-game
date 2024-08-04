using UnityEngine;
using Zenject;

public class FollowingCamera : MonoBehaviour
{
    private ITargetable _target;

    [Header("Camera Properties")] [SerializeField, Range(0, 100)]
    private float _returnSpeed;

    [SerializeField, Range(0, 100)] private float _height;
    [SerializeField, Range(0, 100)] private float _rearDistance;
    [SerializeField, Range(0, 100)] private float _lookOffset;
    
    private Vector3 _currentVector;

    [Inject]
    private void Construct(ITargetable targetable)
    {
        _target = targetable;
    }

    private void Start()
    {
        transform.position = new Vector3(_target.Position.position.x, _target.Position.position.y + _height,
            _target.Position.position.z - _rearDistance);
        transform.rotation = Quaternion.LookRotation(_target.Position.position - transform.position);
        transform.Rotate(_lookOffset, 0, 0);
    }

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        _currentVector = new Vector3(_target.Position.position.x, _target.Position.position.y + _height,
            _target.Position.position.z - _rearDistance);
        transform.position = Vector3.Lerp(transform.position, _currentVector, _returnSpeed * Time.deltaTime);
    }
}