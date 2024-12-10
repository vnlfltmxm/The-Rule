using UnityEngine;

public class InvadeObject : MonoBehaviour
{
    [Header("MyPatrolArea")]
    [SerializeField] private string _areaName;

    [Header("WaitTime")]
    [SerializeField] private float _waitTime;

    [Header("RotationSpeed")]
    [SerializeField] private float _rotationSpeed;

    [Header("ObjectSpeed")]
    [SerializeField] private float _objectSpeed;

    public string AreaName => _areaName;
    public float WaitTime => _waitTime;
    public float RotationSpeed => _rotationSpeed;
    public float ObjectSpeed => _objectSpeed;
}
