using Script.Util;
using UnityEngine;

public class ChildMonster : MonsterBase
{
    private Transform _playerTransform;
    private ChildStateMachine _state;
    
    private void Awake()
    {
        _state = GetComponent<ChildStateMachine>();
        IsInteractable = true;
    }
    
    public override void Interact()
    {
        Debug.Log($"몬스터와 상호작용했다.");
    }
}
