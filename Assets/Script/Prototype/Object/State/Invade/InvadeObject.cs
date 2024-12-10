using UnityEngine;

public class InvadeObject : MonoBehaviour, ISoundTrace
{
    [Header("MyPatrolArea")]
    [SerializeField] private string _areaName;

    [Header("WaitTime")]
    [SerializeField] private float _waitTime;

    [Header("RotationSpeed")]
    [SerializeField] private float _rotationSpeed;

    [Header("ObjectSpeed")]
    [SerializeField] private float _objectSpeed;

    [Header("Hear")]
    [SerializeField] private float _soundDistance;

    [Header("ViewDistance")]
    [SerializeField] private float _viewDistance;

    [Header("ViewAngle")]
    [SerializeField] private float _viewAngle;

    public string AreaName => _areaName;
    public float WaitTime => _waitTime;
    public float RotationSpeed => _rotationSpeed;
    public float ObjectSpeed => _objectSpeed;
    public Vector3 SoundPosition { get; set; }

    private InvadeStateMachine _state;

    private void Awake()
    {
        _state = GetComponent<InvadeStateMachine>();
    }

    public void OnHearSound(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);

        if(distance >= _soundDistance)
        {
            return;
        }

        SoundPosition = position;

        var currentState = _state.GetObjectState();

        if(currentState is InvadeObjectTrace traceState)
        {
            traceState.CalculateTracePath();

            return;
        }
        
        _state.ChangeObjectState(InvadeState.Trace);
    }

   

    private void OnDrawGizmos()
    {
        DrawSoundRange(_soundDistance, transform.position, Color.red);
        DrawView(60f, 10f, 10f);
    }

    private void DrawSoundRange(float radius, Vector3 position, Color color)
    {
        Gizmos.color = color;

        Gizmos.DrawWireSphere(position, radius);
    }

    private void DrawView(float viewAngle, float segments, float range)
    {
        Gizmos.color = Color.blue;

        Vector3 forward = transform.forward;

        for (int i = 0; i <= segments; i++) //��ä�� �� �׸���.
        {
            if (i != 0 && i != segments) //ù ��°, ������ ���� �׸���.
            {
                continue;
            }

            float currentAngle = -viewAngle / 2 + (viewAngle / segments) * i; //���� ��� 60�� -> -30, 30�� ����.

            Quaternion rotation = Quaternion.AngleAxis(currentAngle, transform.up);

            Vector3 direction = rotation * forward;

            Vector3 end = transform.position + direction * range; //�� ���������� �̵�

            Gizmos.DrawLine(transform.position, end);
        }

        for (int i = 0; i < segments; i++) //��ä�� ȣ �׸���
        {
            float angle1 = -viewAngle / 2 + (viewAngle / segments) * i; // ���� ����Ʈ �������.
            float angle2 = -viewAngle / 2 + (viewAngle / segments) * (i+1); // ���� ����Ʈ �������.

            Quaternion rotation1 = Quaternion.AngleAxis(angle1, transform.up);
            Quaternion rotation2 = Quaternion.AngleAxis(angle2, transform.up);

            Vector3 point1 = transform.position + (rotation1 * forward) * range; //���� ������ �̵�
            Vector3 point2 = transform.position + (rotation2 * forward) * range;

            Gizmos.DrawLine(point1, point2);
        }
    }
}
