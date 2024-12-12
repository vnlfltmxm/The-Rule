using UnityEngine;

public class InvadeObjectLook : InvadeObjectState, IObjectState<InvadeObjectLook>, ISetPlayer
{
    public InvadeObjectLook(InvadeObject invadeObject) : base(invadeObject)
    {
        _head = invadeObject.GetComponentInChildren<InvadeHead>();
    }

    public InvadeHead _head;
    private Transform _player;
    
    public void StateEnter()
    {
        _head.StartHeadRotation();
    }

    public void StateUpdate()
    {
        if(_player == null || _head.Completed)
        {
            _state.ChangeObjectState(InvadeState.Patrol);
            return;
        }

        if(CalculateAngle(_player,_head.transform) && CalculateDistance(_player, _head.transform))
        {
            RayCast(_player, _head.transform, InvadeState.Tracking);
        }
    }

    public void StateExit()
    {
        _head.Completed = false;
    }

    protected override void RayCast(Transform targetTransform, Transform originTransform, InvadeState state)
    {
        _rayDirection = (targetTransform.position - originTransform.position).normalized;

        _rayOrigin = originTransform.position + originTransform.TransformDirection(_offSet);

        Debug.DrawRay(_rayOrigin, _rayDirection * (_viewDistance - 1f), Color.red);

        if (Physics.Raycast(_rayOrigin, _rayDirection, out _hit, _viewDistance - 1f))
        {
            if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _head.StopHeadRotation();

                _state.ChangeObjectState(state);
            }
        }
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }
}
