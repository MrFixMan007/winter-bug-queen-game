using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityConfig", menuName = "Configs/EntityConfig")]
public class EntityConfig : ScriptableObject
{
    [field: SerializeField, Range(0, 100), Header("Movement")] public float MoveSpeed { get; private set; }
    [field: SerializeField, Range(0, 200)] public float RunSpeed { get; private set; }
    [field: SerializeField, Range(0, 100)] public float RotationSpeed { get; private set; }
    [field: SerializeField, Range(0, 100)] public float GravityForce { get; private set; }
    
    [field: SerializeField, Range(0, 1000), Header("Health")] public float MaxHp { get; private set; }

    private void OnValidate()
    {
        if (MoveSpeed > RunSpeed) RunSpeed = MoveSpeed + 1;
    }
}