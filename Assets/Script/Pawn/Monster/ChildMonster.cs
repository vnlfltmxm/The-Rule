using Script.Pawn;
using Script.Prop;
using Script.Util;
using UnityEngine;

public class ChildMonster : MonsterBase
{
    private Transform _playerTransform;
    private StateMachine _stateMachine;
    private DialogSystem _dialogSystem;
    
    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _dialogSystem = GetComponent<DialogSystem>();
        IsInteractable = true;
    }

    protected override void Init()
    {
        
    }

    public override void SetActiveForcedEvent(bool isActive)
    {
        
    }

    public override void OnInteract(IInteractor sender)
    {
        Debug.Log($"몬스터와 상호작용했다.");
        _dialogSystem.ActiveDialog();
    }
}
