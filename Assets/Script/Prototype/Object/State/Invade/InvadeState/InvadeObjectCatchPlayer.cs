using UnityEngine;

public class InvadeObjectCatchPlayer : InvadeObjectState, IObjectState<InvadeObjectCatchPlayer>
{
    public InvadeObjectCatchPlayer(InvadeObject invadeObject) : base(invadeObject) { }

    public void StateEnter()
    {
        ForcedEventManager.Instance.PlayerLookAtEvent(_invadeObject.transform);
    }



}
