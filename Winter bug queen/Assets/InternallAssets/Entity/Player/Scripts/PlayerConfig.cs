using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField, Range(0, 100), Header("Movement")] public float MoveSpeed { get; private set; }
    [field: SerializeField, Range(0, 100)] public float RotationSpeed { get; private set; }
    [field: SerializeField, Range(0, 100)] public float GravityForce { get; private set; }
    
    [field: SerializeField, Range(0, 1000), Header("Health")] public float MaxHp { get; private set; }
    
}