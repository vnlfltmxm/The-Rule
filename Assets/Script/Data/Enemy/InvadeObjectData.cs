using UnityEngine;

[CreateAssetMenu(fileName = "InvadeObjectData", menuName = "Scriptable Objects/InvadeObjectData")]
public class InvadeObjectData : ScriptableObject
{
    [Header("구역"), SerializeField]
    private string _area;

    [Header("Idle 정지시간"), SerializeField]
    private float _waitTime;

    [Header("이동속도"), SerializeField]
    private float _speed;

    [Header("회전속도"), SerializeField]
    private float _rotationSpeed;

    [Header("소리를 감지하는 거리"), SerializeField]
    private float _soundDistance;

    [Header("플레이어를 인식하는 거리"), SerializeField]
    private float _detectDistance;

    [Header("플레이어를 감지하는 각도"), SerializeField]
    private float _detectAngle;

    public string Area => _area;
    public float WaitTime => _waitTime;
    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
    public float SoundDistance => _soundDistance;
    public float DetectDistance => _detectDistance;
    public float DetectAngle => _detectAngle;
}
