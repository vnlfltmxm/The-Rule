using System.Collections.Generic;
using Script.Pawn;
using Script.Prop;
using Script.Util;
using UnityEngine;

public class MonsterBase : Pawn, IInteractable
{
    [Header("대기 시간")]
    [SerializeField] private float _waitTime;

    [Header("몬스터 이동시간")]
    [SerializeField] private float _moveSpeed;

    [Header("이동 영역")]
    [SerializeField] private List<AreaType> _areaType;
    
    public float WaitTime => _waitTime;
    public float MoveSpeed => _moveSpeed;
    public List<AreaType> AreaType => _areaType;
    
    public bool IsInteractable { get; set; }
    public virtual void Interact()
    {
        Debug.Log($"몬스터와 상호작용했다.");
    }

    protected override void Init()
    {
        
    }

    public override void SetActiveForcedEvent(bool isActive)
    {
        
    }
}