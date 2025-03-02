using UnityEngine;

public class InvadeObject : MonoBehaviour, ISoundTrace, ISystemTrace, IEnemy
{
    [Header("Data"), SerializeField]
    private InvadeObjectData _data;
    public InvadeObjectData Data => _data;

    public Vector3 SoundPosition { get; set; }

    private Transform _playerTransform;
    private InvadeStateMachine _state;

    private void Awake()
    {
        _state = GetComponent<InvadeStateMachine>();
    }

    private void Start()
    {
        EnemyManager.Instance.RegisterEnemy(EnemyType.Invade, this);
    }

    public void OnHearSound(Vector3 position, Transform player)
    {
        float distance = Vector3.Distance(transform.position, position);

        if(distance >= _data.SoundDistance)
        {
            return;
        }

        var currentState = _state.GetCurrentObjectState();

        SoundPosition = position;

        if(currentState is InvadeObjectTrace
            || currentState is InvadeObjectTracking)
        {
            return;
        }

        SetPlayer(player);

        _state.ChangeObjectState(InvadeState.Trace);
    }

    public void SetPlayer(Transform transform)
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

    public T GetSelf<T>() 
        where T : class
    {
        return this as T;
    }

    public void OnSystemTracking(Transform player)
    {
        throw new System.NotImplementedException();
    }

    #region Gizmo
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        var state = _state.GetCurrentObjectState();
        
        if(state is InvadeObjectLook look)
        {
            DrawView(_data.DetectAngle, 10f, _data.DetectDistance, look._head.transform);

            return;
        }

        DrawSoundRange(_data.SoundDistance, transform.position, Color.red);

        DrawView(_data.DetectAngle, 10f, _data.DetectDistance, transform);
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
