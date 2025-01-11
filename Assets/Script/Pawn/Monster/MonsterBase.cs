using UnityEngine;

public partial class MonsterBase : MonoBehaviour
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
    public float ViewDistance => _viewDistance;
    public float ViewAngle => _viewAngle;
    public Vector3 SoundPosition { get; set; }

    private Transform _playerTransform;
    private InvadeStateMachine _state;

    private void Awake()
    {
        _state = GetComponent<InvadeStateMachine>();
    }
    
    private void SetPlayer(Transform transform)
    {
        if(_playerTransform != null)
        {
            return;
        }

        var stateDictionary = _state.GetStateDictionary();

        foreach(var state in stateDictionary)
        {
            ISetPlayer setplayer = state.Value as ISetPlayer;

            if(setplayer != null)
            {
                setplayer.SetPlayer(transform);
            }
        }
    }

    #region Gizmo
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        var state = _state.GetCurrentObjectState();
        
        if(state is InvadeObjectLook look)
        {
            DrawView(_viewAngle, 10f, _viewDistance, look._head.transform);

            return;
        }

        DrawSoundRange(_soundDistance, transform.position, Color.red);

        DrawView(_viewAngle, 10f, _viewDistance, transform);
    }

    private void DrawSoundRange(float radius, Vector3 position, Color color)
    {
        Gizmos.color = color;

        Gizmos.DrawWireSphere(position, radius);
    }

    private void DrawView(float viewAngle, float segments, float range, Transform transform)
    {
        Gizmos.color = Color.blue;
        
        Vector3 forward = transform.forward;

        for (int i = 0; i <= segments; i++) //부채꼴 선 그리기.
        {
            if (i != 0 && i != segments) //첫 번째, 마지막 선만 그리기.
            {
                continue;
            }

            float currentAngle = -viewAngle / 2 + (viewAngle / segments) * i; //각도 계산 60도 -> -30, 30을 구함.

            Quaternion rotation = Quaternion.AngleAxis(currentAngle, transform.up);

            Vector3 direction = rotation * forward;
            Vector3 end = transform.position + direction * range; //내 포지션으로 이동

            Gizmos.DrawLine(transform.position, end);
        }

        for (int i = 0; i < segments; i++) //부채꼴 호 그리기
        {
            float angle1 = -viewAngle / 2 + (viewAngle / segments) * i; // 현재 포인트 각도계산.
            float angle2 = -viewAngle / 2 + (viewAngle / segments) * (i+1); // 다음 포인트 각도계산.

            Quaternion rotation1 = Quaternion.AngleAxis(angle1, transform.up);
            Quaternion rotation2 = Quaternion.AngleAxis(angle2, transform.up);

            Vector3 point1 = transform.position + (rotation1 * forward) * range; //범위 끝으로 이동
            Vector3 point2 = transform.position + (rotation2 * forward) * range;

            Gizmos.DrawLine(point1, point2);
        }
    }

    #endregion
}
