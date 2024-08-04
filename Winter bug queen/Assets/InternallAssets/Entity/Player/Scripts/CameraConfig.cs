using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/CameraConfig")]
public class CameraConfig : ScriptableObject
{
    [Header("Camera Properties")] [field: SerializeField, Range(0, 100)]
    public float ReturnSpeed;

    [field: SerializeField, Range(0, 100)] public float Height;
    [field: SerializeField, Range(0, 100)] public float RearDistance;
    [field: SerializeField, Range(0, 100)] public float LookOffset;
}