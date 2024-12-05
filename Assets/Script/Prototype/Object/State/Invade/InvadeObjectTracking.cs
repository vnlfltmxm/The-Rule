using UnityEngine;

public class InvadeObjectTracking : InvadeObjectState, IObjectState<InvadeObjectTracking>
{
    public InvadeObjectTracking(InvadeObject invadeObject) : base(invadeObject) { }

    public void StateEnter()
    {
        
    }
}
