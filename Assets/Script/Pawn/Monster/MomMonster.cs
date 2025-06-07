using Script.Pawn;
using Script.Prop;
using Script.Util;
using UnityEngine;

public class MomMonster : MonsterBase
{
    private StateMachine _stateMachine;
    
    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
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
        UIManager.Instance.GetUI<DialogSelectionUI>().ShowChoices();
    }
}
