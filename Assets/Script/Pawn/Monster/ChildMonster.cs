using UnityEngine;

public class ChildMonster : MonoBehaviour
{
    [Header("WaitTime")]
    [SerializeField] private float _waitTime;

    [Header("ObjectSpeed")]
    [SerializeField] private float _objectSpeed;

    
    public float WaitTime => _waitTime;
    public float ObjectSpeed => _objectSpeed;

    private Transform _playerTransform;
    private ChildStateMachine _state;
    
    private void Awake()
    {
        _state = GetComponent<ChildStateMachine>();
    }
}
