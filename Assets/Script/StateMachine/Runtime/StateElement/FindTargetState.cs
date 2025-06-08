using System;
using Script.Pawn;

[Serializable, StateElementType(typeof(AttackState))]
public class FindTargetState : StateElement
{
    //private FindTargetStateData _data;
    
    

    public override void Enter()
    {
        /*base.Enter();

        // _targetLayer 해당하는 오브젝트들만 감지
        Collider[] hits = Physics.OverlapSphere(StateObject.transform.position, _data.Distance, LayerMask.GetMask("InvadeObject"));
        float minDistance = float.MaxValue;
        Pawn closest = null;
        
        foreach (var hit in hits)
        {
            Pawn pawn = hit.GetComponent<Pawn>();
            if(StateObject == pawn)
                continue;
            AreaType pawnArea = pawn.GetCurrentArea();
            if (_data.AreaTypes.Any(type => type != pawnArea))
                continue;
            float distance = Vector3.Distance(StateObject.transform.position, pawn.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = pawn;
            }
        }
        if (closest != null)
        {
            StateObject.Target = closest;
        }*/
    }

    public override void Stay()
    {
    }

    public override void Exit()
    {
    }
}
