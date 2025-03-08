using Script.Prop;
using Script.Util;
using UnityEngine;

public class MonsterBase : MonoBehaviour, IInteractable
{
    [Header("WaitTime")]
    [SerializeField] private float _waitTime;

    [Header("ObjectSpeed")]
    [SerializeField] private float _moveSpeed;

    [Header("AreaType")]
    [SerializeField] private AreaType _areaType;
    
    public float WaitTime => _waitTime;
    public float MoveSpeed => _moveSpeed;
    public AreaType AreaType => _areaType;
    
    public bool IsInteractable { get; set; }
    public virtual void Interact()
    {
        Debug.Log($"몬스터와 상호작용했다.");
    }
}