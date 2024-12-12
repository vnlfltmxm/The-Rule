using UnityEngine;

public class InvadeObjectState
{
    public InvadeObjectState(InvadeObject invadeObject)
    {
        _invadeObject = invadeObject;
        _state = invadeObject.GetComponent<InvadeStateMachine>();
   
        InitializeFieldOfView(invadeObject);
    }

    protected InvadeObject _invadeObject;
    protected InvadeStateMachine _state;
   
    #region FieldOfView
    private void InitializeFieldOfView(InvadeObject invadeObject)
    {
        _viewAngle = invadeObject.ViewAngle;
        _viewDistance = invadeObject.ViewDistance;
    }

    protected float _viewDistance;
    protected float _viewAngle;

    protected RaycastHit _hit;
    protected Vector3 _offSet = new Vector3(0f, 1f, 1f);
    protected Vector3 _rayDirection;
    protected Vector3 _rayOrigin;

    protected bool CalculateAngle(Transform targetTransform, Transform originTransform)
    {
        Vector3 angleDirection = (targetTransform.position - originTransform.position).normalized;

        Vector3 forward = originTransform.forward;

        float angle = Vector3.Angle(forward, angleDirection);

        if (angle < _viewAngle / 2)
        {
            return true;
        }

        return false;
    }

    protected bool CalculateDistance(Transform targetTransform, Transform originTransform)
    {
        bool distance = Vector3.Distance(targetTransform.position, originTransform.position) <= _viewDistance;

        return distance;
    }

    protected virtual void RayCast(Transform targetTransform, Transform originTransform, InvadeState state)
    {
        _rayDirection = (targetTransform.position - originTransform.position).normalized;

        _rayOrigin = originTransform.position + originTransform.TransformDirection(_offSet);

        Debug.DrawRay(_rayOrigin, _rayDirection * (_viewDistance - 1f), Color.red);

        if (Physics.Raycast(_rayOrigin, _rayDirection, out _hit, _viewDistance - 1f))
        {
            if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _state.ChangeObjectState(state);
            }
        }
    }
    #endregion
}
