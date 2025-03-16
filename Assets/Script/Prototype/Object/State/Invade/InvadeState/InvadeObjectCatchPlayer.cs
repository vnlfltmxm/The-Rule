using UnityEngine;

public class InvadeObjectCatchPlayer : InvadeObjectState, IObjectState<InvadeObjectCatchPlayer>, ISetPlayer
{
    public InvadeObjectCatchPlayer(InvadeObject invadeObject) : base(invadeObject) { }

    private Transform _player;

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void StateEnter()
    {

    }

}
