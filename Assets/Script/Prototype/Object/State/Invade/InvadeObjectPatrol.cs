using UnityEngine;

public class InvadeObjectPatrol : InvadeObjectState, IObjectState<InvadeObjectPatrol>
{
    public InvadeObjectPatrol(InvadeObject invadeObject) : base(invadeObject) { }

    public void StateEnter()
    {
        Debug.Log("¼øÂû¼øÂû!");
    }
}
