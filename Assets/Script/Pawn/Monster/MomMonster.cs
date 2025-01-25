using UnityEngine;

public class MomMonster : MonoBehaviour
{
    [Header("WaitTime")]
    [SerializeField] private float _waitTime;

    [Header("ObjectSpeed")]
    [SerializeField] private float _moveSpeed;
    
    public float WaitTime => _waitTime;
    public float MoveSpeed => _moveSpeed;

    private MomStateMachine _state;
    
    private void Awake()
    {
        _state = GetComponent<MomStateMachine>();
    }
}
