using System;
using LN;
using Script.Manager.Framework;
using Script.Pawn;
using UnityEngine;

public class LookState : State
{
    public InvadeHead _head;
    private Transform _target;
    
    private float _viewAngle;
    private float _viewDistance;
    private Vector3 _offSet = new Vector3(0f, 1f, 1f);
    private LayerMask _targetLayer = LayerMask.GetMask("Player");

    public override void InitState(Pawn stateObject)
    {
        base.InitState(stateObject);
        _head = _stateObject.GetComponentInChildren<InvadeHead>();
    }

    public override void StateEnter()
    {
        
        _head.StartHeadRotation();
    }
    public override Type StateCheck()
    {
        if (_head == null)
            return typeof(IdleState);
        
        if (_target == null || _head.Completed)
            return typeof(PatrolState);
        return null;
    }
    public override void StateUpdate()
    {
        DrawView(_viewAngle, 10f, _viewDistance, _head.transform);
        if(DetectionUtils.CalculateAngle(_head.transform, _target, _viewAngle) && DetectionUtils.CalculateDistance(_head.transform, _target, _viewAngle))
        {
            DetectionUtils.RayCast(_head.transform, _target, _viewDistance, _offSet, _targetLayer);
        }
    }
    public override void StateExit()
    {
        _head.StopHeadRotation();

        _head.Completed = false;
    }

    public override void OnStateDrawGizmos()
    {
        DrawView(_viewAngle, 10f, _viewDistance, _head.transform);
    }
    
    private void DrawView(float viewAngle, float segments, float range, Transform transform)
    {
        Gizmos.color = Color.blue;
        Vector3 forward = transform.forward;
        
        for (int i = 0; i <= segments; i++) //부채꼴 선 그리기.
        {
            if (i != 0 && i != segments) //첫 번째, 마지막 선만 그리기.
            {
                continue;
            }

            float currentAngle = -viewAngle / 2 + (viewAngle / segments) * i; //각도 계산 60도 -> -30, 30을 구함.

            Quaternion rotation = Quaternion.AngleAxis(currentAngle, transform.up);

            Vector3 direction = rotation * forward;
            Vector3 end = transform.position + direction * range; //내 포지션으로 이동

            Gizmos.DrawLine(transform.position, end);
        }

        for (int i = 0; i < segments; i++) //부채꼴 호 그리기
        {
            float angle1 = -viewAngle / 2 + (viewAngle / segments) * i; // 현재 포인트 각도계산.
            float angle2 = -viewAngle / 2 + (viewAngle / segments) * (i+1); // 다음 포인트 각도계산.

            Quaternion rotation1 = Quaternion.AngleAxis(angle1, transform.up);
            Quaternion rotation2 = Quaternion.AngleAxis(angle2, transform.up);

            Vector3 point1 = transform.position + (rotation1 * forward) * range; //범위 끝으로 이동
            Vector3 point2 = transform.position + (rotation2 * forward) * range;

            Gizmos.DrawLine(point1, point2);
        }
    }
}
