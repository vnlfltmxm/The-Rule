using Script.Util;
using UnityEngine;

public class MomMonster : MonsterBase
{
    private MomStateMachine _state;
    
    private void Awake()
    {
        _state = GetComponent<MomStateMachine>();
        IsInteractable = true;
    }
    
    public override void Interact()
    {
        UIManager.Instance.UICanvas
    }
}
