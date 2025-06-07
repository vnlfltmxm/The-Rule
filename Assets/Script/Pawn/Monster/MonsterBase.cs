using System.Collections.Generic;
using Script.Pawn;
using Script.Prop;
using Script.Util;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : Pawn, IInteractable
{
    public NavMeshAgent Agent;
    
    [Header("이동 영역")]
    [SerializeField] private List<AreaType> _areaType;
    
    public List<AreaType> AreaType => _areaType;
    
    public bool IsInteractable { get; set; }
    public virtual void OnInteract(IInteractor sender)
    {
        Debug.Log($"몬스터와 상호작용했다.");
    }
    
    protected override void Init()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.enabled = true;
    }

    public override void SetActiveForcedEvent(bool isActive)
    {
        
    }
}
