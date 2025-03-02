using UnityEngine;

[CreateAssetMenu(fileName = "InvadeObjectData", menuName = "Scriptable Objects/InvadeObjectData")]
public class InvadeObjectData : ScriptableObject
{
    [Header("����"), SerializeField]
    private string _area;

    [Header("Idle �����ð�"), SerializeField]
    private float _waitTime;

    [Header("�̵��ӵ�"), SerializeField]
    private float _speed;

    [Header("ȸ���ӵ�"), SerializeField]
    private float _rotationSpeed;

    [Header("�Ҹ��� �����ϴ� �Ÿ�"), SerializeField]
    private float _soundDistance;

    [Header("�÷��̾ �ν��ϴ� �Ÿ�"), SerializeField]
    private float _detectDistance;

    [Header("�÷��̾ �����ϴ� ����"), SerializeField]
    private float _detectAngle;

    public string Area => _area;
    public float WaitTime => _waitTime;
    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
    public float SoundDistance => _soundDistance;
    public float DetectDistance => _detectDistance;
    public float DetectAngle => _detectAngle;
}
